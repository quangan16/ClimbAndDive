Shader "TransitionsPlus/Mosaic" {
	Properties {
		[HideInInspector] _T ("Progress", Range(0, 1)) = 0
		[HideInInspector] _MainTex ("Texture", 2D) = "white" {}
		[HideInInspector] _NoiseTex ("Noise", 2D) = "white" {}
		[HideInInspector] _GradientTex ("Gradient Tex", 2D) = "white" {}
		[HideInInspector] _Color ("Color", Vector) = (0,0,0,1)
		[HideInInspector] _VignetteIntensity ("Vignette Intensity", Range(0, 1)) = 0.5
		[HideInInspector] _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.5
		[HideInInspector] _ToonIntensity ("Toon Intensity", Float) = 1
		[HideInInspector] _ToonDotIntensity ("Toon Dot Intensity", Float) = 1
		[HideInInspector] _AspectRatio ("Aspect Ratio", Float) = 1
		[HideInInspector] _Distortion ("Distortion", Float) = 1
		[HideInInspector] _ToonDotRadius ("Toon Dot Radius", Float) = 0
		[HideInInspector] _ToonDotCount ("Toon Dot Count", Float) = 0
		[HideInInspector] _Contrast ("Constrast", Float) = 1
		[HideInInspector] _CellDivisions ("Cell Divisions", Float) = 32
		[HideInInspector] _Spread ("Spread", Float) = 64
		[HideInInspector] _RotationMultiplier ("Rotation", Float) = 0
		[HideInInspector] _Rotation ("Rotation", Float) = 0
		[HideInInspector] _Splits ("Splits", Float) = 2
		[HideInInspector] _Seed ("Seed", Float) = 0
		[HideInInspector] _CentersCount ("Seed", Float) = 1
		[HideInInspector] _TimeMultiplier ("Time Multiplier", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}