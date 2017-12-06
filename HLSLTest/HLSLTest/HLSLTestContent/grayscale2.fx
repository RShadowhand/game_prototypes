float4x4 World;
float4x4 View;
float4x4 Projection;

float Epsilon = 1e-10;
 
float3 RGBtoHCV(in float3 RGB)
{
	// Based on work by Sam Hocevar and Emil Persson
	float4 P = (RGB.g < RGB.b) ? float4(RGB.bg, -1.0, 2.0/3.0) : float4(RGB.gb, 0.0, -1.0/3.0);
	float4 Q = (RGB.r < P.x) ? float4(P.xyw, RGB.r) : float4(RGB.r, P.yzx);
	float C = Q.x - min(Q.w, Q.y);
	float H = abs((Q.w - Q.y) / (6 * C + Epsilon) + Q.z);
	return float3(H, C, Q.x);
}
float3 HUEtoRGB(in float H)
{
	float R = abs(H * 6 - 3) - 1;
	float G = 2 - abs(H * 6 - 2);
	float B = 2 - abs(H * 6 - 4);
	return saturate(float3(R,G,B));
}
float3 RGBtoHSV(in float3 RGB)
{
	float3 HCV = RGBtoHCV(RGB);
	float S = HCV.y / (HCV.z + Epsilon);
	return float3(HCV.x, S, HCV.z);
}
float3 HSVtoRGB(in float3 HSV)
{
	float3 RGB = HUEtoRGB(HSV.x);
	return ((RGB - 1) * HSV.y + 1) * HSV.z;
}

sampler s0;
float4 zero(float2 coords: TEXCOORD0) : COLOR0  
{  
    float4 color = tex2D(s0, coords);
	return color;
} 

float4 plus(float2 coords: TEXCOORD0) : COLOR0  
{  
    float4 color = tex2D(s0, coords);

	float3 hsv = RGBtoHSV(float3(color.r, color.g, color.b));
	hsv.x = hsv.x + 0.025f;
	//hsv.z = hsv.z + 0.3f;
	float3 buffer = HSVtoRGB(hsv);
	color = float4(buffer.x, buffer.y, buffer.z, color.a);

	return color;
} 

float4 minus(float2 coords: TEXCOORD0) : COLOR0  
{  
    float4 color = tex2D(s0, coords);

	float3 hsv = RGBtoHSV(float3(color.r, color.g, color.b));
	hsv.x = hsv.x - 0.025f;
	//hsv.z = hsv.z + 0.3f;
	float3 buffer = HSVtoRGB(hsv);
	color = float4(buffer.x, buffer.y, buffer.z, color.a);

	return color;
}  
      
technique Technique1  
{  
	pass Pass1
	{
		PixelShader = compile ps_2_0 zero();  
	}
    pass Pass2 
    {  
        PixelShader = compile ps_2_0 plus();  
    }
	pass Pass3
	{
		PixelShader = compile ps_2_0 minus();  
	}
}  
