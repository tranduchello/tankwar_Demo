#pragma strict
var Objectman : Transform;
var timeSpawn = 0;
var timeSpawnMax : int;
var enemyCount = 0;
var radiun :int;


function Start () {
if(GetComponent.<Renderer>())
  GetComponent.<Renderer>().enabled = false;

}

function Update () {
   var gos : GameObject[];
   gos = GameObject.FindGameObjectsWithTag("Enemy");
   timeSpawn+=1;
   if(gos.length < enemyCount){
   var timespawnmax = timeSpawnMax;
   if(timespawnmax<=0){
   		timespawnmax = 10;
   }
   if(timeSpawn>=timespawnmax){
   	  var enemyCreated = Instantiate(Objectman, transform.position+ new Vector3(Random.Range(-radiun,radiun),20,Random.Range(-radiun,radiun)), Quaternion.identity);

   	  enemyCreated.transform.localScale.x = Random.Range(5,20);
	  enemyCreated.transform.localScale.y = enemyCreated.transform.localScale.x;
	  enemyCreated.transform.localScale.z = enemyCreated.transform.localScale.x;
	  
   	  timeSpawn = 0;
   	  
   }
   }
   
}
