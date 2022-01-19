using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AutoPlayer : MonoBehaviour
{
    public bool cursorVisible;
    public float resetHeight = -5;
    public int force = 15;
    public Vector3 worldPosition;
    public float speed = 4;
    public bool grounded;
    int index;
    Rigidbody rb;
    LayerMask layerMask = -1;
    bool isLeft = false;
    bool isRight = false;
    public Queue<string> actions = new Queue<string>();
    private Grid grid;
    public int x_int = 1;
    public Vector3 checkpoint_pos;
    public Quaternion checkpoint_rot;
    private string[] action_tags = { "Jump", "Projectile", "Left", "Right", "Left_Dodge", "Right_Dodge" };
	public bool fx_muted = true;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }


    void Start()
    {
        Time.fixedDeltaTime = 1 / 100f;
        rb = GetComponent<Rigidbody>();
        layerMask &= ~(1 << gameObject.layer);
        InvokeRepeating("Clock", 1, 1);
        Cursor.visible = cursorVisible;
        transform.position = grid.GetNearestPointOnGrid(transform.position);
        checkpoint_pos = transform.position;
        checkpoint_rot = transform.rotation;

    }

    void OnGUI()
    {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    }


    void Update()
    {


        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touchCount > 0 ? Input.touches[0].fingerId : -1))
            {
                Debug.Log("Clicked on the UI");
            }
            else
            {
                if (actions.Count > 0)
                {
                    addAction();
                }
            }

        }
            
        
       
        
    }

    private Transform actionContainer;
    private Transform action;
    private Transform actionList;

    public void updateActions()
    {
        actionContainer = GameObject.Find("Actions").GetComponent<Transform>();
        action = actionContainer.Find("Template");
        actionList = actionContainer.Find("ActionContainer");
        foreach (Transform child in actionList)
        {
            Destroy(child.gameObject);
        }
        
        string[] action_array = actions.ToArray();

        for (int i = 0; i < action_array.Length; i++)
        {
            Transform actionTransform = Instantiate(action, actionList);
            RectTransform actionRectTransform = actionTransform.GetComponent<RectTransform>();
            actionRectTransform.anchoredPosition = new Vector2(190 * i, 0);
            actionTransform.Find("Text").GetComponent<Text>().text = getAction(action_array[i]);
            actionTransform.gameObject.SetActive(true);
        }
    }


    void addAction()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Ground" && actions.Count > 0)
                {
                    worldPosition = ray.GetPoint(distance);
                    var finalPosition = grid.GetNearestPointOnGrid(worldPosition);
                    finalPosition.y += .5f;
                    string curr_action = actions.Dequeue();
                    GameObject action = Instantiate(Resources.Load(curr_action), finalPosition, transform.rotation) as GameObject;
                    PlaySound(false, "IconPlace");
                }
            }

        }
        
        updateActions();
    }



    string getAction(string action)
    {
        if (action.Equals("Left"))
        {
            return "Left Turn";
        }
        else if (action.Equals("Right"))
        {
            return "Right Turn";
        }
        else if (action.Equals("Left_Dodge"))
        {
            return "Left Dodge";
        }
        else if (action.Equals("Right_Dodge"))
        {
            return "Right Dodge";
        }
        else
        {
            return action;
        }
    }

    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(transform.position - transform.up * 0.2f, 0.4f, layerMask.value);
        Vector3 velocity = transform.TransformDirection(Vector3.forward) * speed;
        velocity.y = rb.velocity.y;
        if (grounded) { rb.velocity = velocity; }
        rb.AddForce(Physics.gravity * 2, ForceMode.Force);
    }

    void OnTriggerEnter(Collider c)
    {
        

        if (c.gameObject.tag == "Jump")
        {
            if (grounded)
            {
                grounded = false;
                rb.AddForce(0, force, 0, ForceMode.Impulse);
                PlaySound(true, "Jump");
                Destroy(c.gameObject);
            }
        }

        if (c.gameObject.tag == "Projectile")
        {

            GameObject projectile = Instantiate(Resources.Load("Bullet"), transform.position, transform.rotation) as GameObject;
            //Launch the projectile forward at a speed of 300;
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
            PlaySound(true, "Shoot");
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "Left")
        {
            gameObject.transform.Rotate(0, -90, 0);
            
            transform.position = grid.GetNearestPointOnGrid(transform.position);
            PlaySound(true, "Turn");
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "Right")
        {
            gameObject.transform.Rotate(0, 90, 0);
            transform.position = grid.GetNearestPointOnGrid(transform.position);
            PlaySound(true, "Turn");
            Destroy(c.gameObject);
        }
        if (c.gameObject.tag == "Wall")
        {
            PlaySound(false, "CharVWall");
            checkPoint();
        }

        if (c.gameObject.tag == "Barrier")
        {
            PlaySound(false, "CharVWall");
            checkPoint();
        }


        if (c.gameObject.tag == "Left_Dodge")
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * 125, Space.Self);
            transform.position = grid.GetNearestPointOnGrid(transform.position);
            PlaySound(true, "Dash");
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "Right_Dodge")
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * 125, Space.Self);
            transform.position = grid.GetNearestPointOnGrid(transform.position);
            PlaySound(true, "Dash");
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "Checkpoint")

        {
            checkpoint_pos = grid.GetNearestPointOnGrid(c.gameObject.transform.position);
            checkpoint_rot = transform.rotation;
        }

        if (c.gameObject.tag == "Death")
        {
            PlaySound(false, "Fall");
            checkPoint();

        }
        if (c.gameObject.tag == "Goal")
        {
            PlaySound(false, "Goal");
            SceneManager.LoadScene(0);

        }

    }

    public void PlaySound(bool actionSound, string effect)
    {
        if (!fx_muted)
		{
            var SFX = GameObject.Find("SFX").GetComponent<Transform>();
            Transform sound_obj;
            if (actionSound)
            {
                var sounds = SFX.Find("Function Sounds");
                sound_obj = sounds.Find(effect);
            
            
            } else
            {
                sound_obj = SFX.Find(effect);
            }
            var sound = sound_obj.GetComponent<AudioSource>();
		    sound.Play();
		}
            
    }

    void checkPoint()
    {
        for (int i = 0; i < action_tags.Length; i++)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(action_tags[i]);
            foreach (GameObject go in gos)
                Destroy(go);
        }

        transform.position = checkpoint_pos;
        transform.rotation = checkpoint_rot;
        actions.Clear();
        updateActions();
    }
    

    void Clock()
    {
        if (transform.position.y < resetHeight) { checkPoint(); }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(0, resetHeight, 0), new Vector3(50, 0, 50));
    }
}
