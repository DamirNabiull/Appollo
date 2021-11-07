sampler2D input : register(s0);
sampler2D blend : register(s1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 clr1;
	clr1= tex2D(blend, uv.xy);

	float4 Color;
	Color= tex2D( input , uv.xy);

	Color.r=clr1.r * Color.r;
	Color.g=clr1.g * Color.g;
	Color.b=clr1.b * Color.b;

	return Color;
}