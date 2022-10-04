shader_type canvas_item;

uniform vec4 tint: source_color = vec4(1.0);
uniform float flicker = 0.0;


float absine(float val){
	return (sin(val) + 1.) * .5;
}

void fragment() {
	float raw = (absine(TIME * 2.) * absine(TIME * 2.5)) * (absine(TIME * 5.) * absine(TIME * 5.5));
	float fade = max(raw, 1. - flicker);
	COLOR = texture(SCREEN_TEXTURE, SCREEN_UV) * tint;
	//COLOR = COLOR * 2.;
	COLOR.a = texture(TEXTURE, UV).a * fade;
}
