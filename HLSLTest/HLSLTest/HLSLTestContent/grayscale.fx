float4x4 World;
float4x4 View;
float4x4 Projection;

sampler2D Texture0 : register(s0);
     
float4 main(float2 uv : TEXCOORD0) : COLOR
{
	float4 colorin = tex2D(Texture0, uv);
	float4 buffer, colorout, bwcolor;
	buffer = colorin;
	if(colorin.a){
		colorin.rb = colorin.g;
		bwcolor = colorin;
		colorin = buffer;
		float weight = smoothstep(0.15, 0.35f, colorin.r - bwcolor);
		colorout = lerp(bwcolor, colorin * float4(1.1f, 0.5f, 0.5f, 1.f), weight);
	}
	return pow(colorout, .85f);
}


technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 main();
    }
}
