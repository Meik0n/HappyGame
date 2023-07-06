using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {MovingToNode, Shooting, Spawning, Following};
public enum Stage {First, Second, Third};

public class HadaScript : MonoBehaviour {

    public State CurrentState = State.MovingToNode;
    public Stage CurrentStage = Stage.First;
    public GameObject Path;
    public GameObject EnemyPrefab;
    public GameObject ShootPrefabRange;
    public GameObject ShootPrefabFollow;
    public float speed = 5;
    private List<Transform> Nodes = new List<Transform>();
    private int Cycles = 0;
    private GameObject SpawnedEnemy;
    public int Life = 10;

	void Start () {
        int i = 0;
		for(i = 0; i < Path.transform.childCount; i++)
        {
            Nodes.Add(Path.transform.GetChild(i).transform);
        }
        StartCoroutine(MovingToNode());
        StartCoroutine(Spawning());
        StartCoroutine(Shooting());
        StartCoroutine(Following());
	}
	
	void Update () {
		if(CurrentStage == Stage.First)
        {
            if (Cycles == 6)
            {
                Cycles = 0;
                CurrentState = State.Spawning;
            }
            if(Life < 7)
            {
                CurrentStage = Stage.Second;
                CurrentState = State.MovingToNode;
            }
        }
        else if(CurrentStage == Stage.Second)
        {
            if (Cycles == 3)
            {
                Cycles = 0;
                CurrentState = State.Spawning;
            }
            if (Life < 4)
            {
                CurrentStage = Stage.Third;
                CurrentState = State.Following;
            }
        }
        else if(CurrentStage == Stage.Third)
        {
            if(Cycles == 200)
            {
                CurrentState = State.Shooting;
            }
            if(Cycles == 400)
            {
                CurrentState = State.Spawning;
            }
        }
        if (Life <= 0)
        {
            StopAllCoroutines();
            gameObject.transform.parent.GetComponent<Room>().EnemiesInRoom.Remove(gameObject);
            Destroy(gameObject);
        }
        if(CurrentState == State.Spawning)
        {
            GetComponent<Animator>().SetBool("spawning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("spawning", false);
        }
    }

    IEnumerator MovingToNode()
    {
        while (true)
        {
            if(CurrentState == State.MovingToNode)
            {
                if(CurrentStage != Stage.Third)
                {
                    foreach (Transform t in Nodes)
                    {
                        Cycles++;
                        while (Vector2.Distance(t.position, transform.position) > 0.01f)
                        {
                            if (CurrentStage == Stage.Third)
                            {
                                break;
                            }
                            transform.position = Vector3.Lerp(transform.position, t.position, speed * Time.deltaTime);
                            yield return null;
                        }
                        if (CurrentStage == Stage.Second && Cycles % 3 == 0)
                        {
                            Instantiate(ShootPrefabFollow, transform.position, Quaternion.identity);
                        }
                    }
                }
            }
            yield return null;
        }
    }

    IEnumerator Spawning()
    {
        while (true)
        {
            if(CurrentState == State.Spawning)
            {
                if(CurrentStage == Stage.First)
                {
                    if (SpawnedEnemy == null)
                    {
                        yield return new WaitForSeconds(1);
                        SpawnedEnemy = Instantiate(EnemyPrefab, Path.transform.parent.transform);
                        yield return new WaitForSeconds(2);
                        CurrentState = State.MovingToNode;
                    }
                    else
                    {
                        yield return new WaitForSeconds(2);
                        CurrentState = State.MovingToNode;
                    }
                }
                if(CurrentStage == Stage.Second)
                {
                    if (SpawnedEnemy == null)
                    {
                        yield return new WaitForSeconds(1);
                        SpawnedEnemy = Instantiate(EnemyPrefab, Path.transform.parent.transform);
                        yield return new WaitForSeconds(2);
                        CurrentState = State.Shooting;
                    }
                    else
                    {
                        yield return new WaitForSeconds(2);
                        CurrentState = State.MovingToNode;
                    }
                }
                if (CurrentStage == Stage.Third)
                {
                    if (SpawnedEnemy == null)
                    {
                        yield return new WaitForSeconds(2);
                        SpawnedEnemy = Instantiate(EnemyPrefab, Path.transform.parent.transform);
                        CurrentState = State.Shooting;
                    }
                    else
                    {
                        yield return new WaitForSeconds(2);
                        CurrentState = State.Following;
                    }
                    Cycles = 0;
                }
            }
            yield return null;
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            if(CurrentState == State.Shooting)
            {
                if(CurrentStage == Stage.Second)
                {
                    Instantiate(ShootPrefabFollow, transform.position, Quaternion.identity);
                    CurrentState = State.MovingToNode;
                }
                if(CurrentStage == Stage.Third)
                {
                    Instantiate(ShootPrefabFollow, new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), Quaternion.identity);
                    Instantiate(ShootPrefabFollow, new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z), Quaternion.identity);
                    Instantiate(ShootPrefabFollow, new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z), Quaternion.identity);
                    Instantiate(ShootPrefabFollow, new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z), Quaternion.identity);
                    CurrentState = State.Following;
                }
            }
            yield return null;
        }
    }

    IEnumerator Following()
    {
        while (true)
        {
            if (CurrentState == State.Following)
            {
                Debug.Log("following");
                Cycles++;
                transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 2 * Time.deltaTime);
            }
            yield return null;
        }
    }

    public void HurtFairy()
    {
        if(CurrentState == State.Spawning)
        {
            Life--;
            if(CurrentStage == Stage.First)
            {
                CurrentState = State.MovingToNode;
            }
            else if(CurrentStage == Stage.Second)
            {
                CurrentState = State.MovingToNode;
            }
            else
            {
                CurrentState = State.Following;
            }
        }
    }
}
