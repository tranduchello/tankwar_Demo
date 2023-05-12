var hitsound: AudioClip[];
var effect:GameObject;
var HP:int = 100;

function Start(){

}

function ApplyDamage (damage : float) {

	if(hitsound.Length>0){
	 	AudioSource.PlayClipAtPoint(hitsound[Random.Range(0,hitsound.length)], transform.position);
	}
 	HP -= damage;
 	if(HP<=0){
 		Dead();
 	}
}
function Dead(){
	if(effect)
	Instantiate(effect,this.transform.position,this.transform.rotation);
	Destroy(this.gameObject);
}
