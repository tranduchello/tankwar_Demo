var speed:float = 20;
var turnspeed:float = 100;
function Start () {

}

function Update () {
	
	this.transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal")* turnspeed * Time.deltaTime,0));
	this.transform.position += this.transform.forward * Input.GetAxis("Vertical") *speed* Time.deltaTime;
}