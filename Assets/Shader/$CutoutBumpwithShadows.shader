Shader "Transparent/Bumped Diffuse with Shadow" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_BumpMap2 ("Normalmap2", 2D) = "bump2" {}
	_Cutoff("Cutoff", Float) = 0.01
}

SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="TransparentCutOut" }
	LOD 300
	Cull off

		CGPROGRAM
	#pragma surface surf Lambert addshadow alphatest:_Cutoff
	
	sampler2D _MainTex;
	sampler2D _BumpMap;
	sampler2D _BumpMap2;
	fixed4 _Color;
	
	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
		float2 uv_BumpMap2;
	};
	
	void surf (Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		//o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		// Add the two normals from input together
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap) + tex2D(_BumpMap2, IN.uv_BumpMap2) * 2 - 1);
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG
}

Fallback "Transparent/Diffuse"
}