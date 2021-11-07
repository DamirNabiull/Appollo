sampler2D input : register(s0);
sampler2D blend : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 clr1;
	clr1= tex2D(blend, uv.xy);

	float4 Color;
	Color= tex2D( input , uv.xy);

	float clr2;
	clr2= dot(clr1.rgb, float3(0.3f, 0.59f, 0.11f));

	Color.r= Color.r * clr2;
	Color.g= Color.g * clr2;
	Color.b= Color.b * clr2;

	return Color;
}