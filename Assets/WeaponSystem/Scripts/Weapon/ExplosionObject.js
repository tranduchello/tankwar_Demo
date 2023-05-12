
var Force:Vector3;
var Objcet:GameObject;
var Num:int;
var Scale:int = 20;
var Sounds:AudioClip[];
var LifeTimeObject:float = 2;
function Start () {

	if(Sounds.length>0){
		AudioSource.PlayClipAtPoint(Sounds[Random.Range(0,Sounds.length)],transform.position);
	}
	if(Objcet){
	for(var i=0;i<Num;i++){
		var pos = new Vector3(Random.Range(-10,10),Random.Range(-10,20),Random.Range(-10,10)) / 10f;
		var obj:GameObject = Instantiate(Objcet, this.transform.position + pos, this.transform.rotation);
		var scale = Random.Range(1,Scale);
		Destroy(obj,LifeTimeObject);
		if(Scale>0)
		obj.transform.localScale = new Vector3(scale,scale,scale);
		if(obj.GetComponent.<Rigidbody>() ){
   			obj.GetComponent.<Rigidbody>().AddForce(new Vector3(Random.Range(-Force.x,Force.x),Random.Range(-Force.y,Force.y),Random.Range(-Force.z,Force.z)));
   		}
   		
  		}
  	}
}
