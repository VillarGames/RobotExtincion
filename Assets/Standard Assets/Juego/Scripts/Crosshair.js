@script ExecuteInEditMode()
var Mira : Texture2D;
var Alto = 0.3;
var Largo = 0.7;

function OnGUI () {	
	var w = Mira.width;
	var h = Mira.height;
	position = Rect((Screen.width - w)/2,(Screen.height - h )/2, w, h);
	
	if (!Input.GetButton ("Fire2")) { 
		GUI.DrawTexture(position, Mira);
	}
}	