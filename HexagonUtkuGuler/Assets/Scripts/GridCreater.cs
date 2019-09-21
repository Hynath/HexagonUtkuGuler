using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreater : MonoBehaviour
{
    public int Vertical = 5;
    public int Horizontal = 5;
    public float HorDistance = 0.1f;
    public float VerDistance = 0.1f;
    public Vector2 StartingPointOffset = new Vector2(0,0);
    public float LittleDistance;
    public float BigDistance;
    public GameObject SpawndEntity;
    public GameObject RotatorEntity;

    private int nameBufferRot = 0;
    private int nameBufferHex = 0;

    Vector3 PosCursor = new Vector3(0, 0, 0);
    Vector3 Pos = new Vector3(0, 0, 0);
    Quaternion Rot = new Quaternion(0, 0, 0, 0);
    static bool buffer = true;


    public static int OngoingDropCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateHexes();
        CreateRotators();

    }

    // Update is called once per frame
    void Update()
    {
       
        if(buffer == true)
        {
            CheckAllRotators();
            buffer = false;
        }
    }

    void CreateHexes()
    {

        int HorizontalHex = (Horizontal *2);
       
        for (int l = 0; l < HorizontalHex; l++)
        {
            for (int i = 0; i < Vertical; i++)
            {
                if (i % 2 == l % 2) //if both coordinats are round or odd spawn a hex
                {

                    Pos.Set(StartingPointOffset.x + (i * (1.5f * HorDistance)), StartingPointOffset.y + (l * VerDistance), 0);
                    Rot.Set(0, 0, 0, 0);
                    GameObject HexEntity =Instantiate(SpawndEntity, Pos, Rot);
                    nameBufferHex++;
                    HexEntity.name =(nameBufferHex).ToString();
                }
            }
        }
    }

    void CreateRotators()
    {
        
        int HorizontalHexNumber = Horizontal*2 ;
        int buffer = 0;
        float RotatorCursorX = 0;


        for(int i= 1; i < HorizontalHexNumber - 1; i++)
        {
            buffer = i%2;
            if(buffer == 0)            
                RotatorCursorX = 4;
            else
                RotatorCursorX = 3;


            PosCursor.Set(StartingPointOffset.x + RotatorCursorX, StartingPointOffset.y + (i * VerDistance), 0);
            for (int l = 0; l< Vertical-1; l++)
            {
                GameObject RotatorObject = Instantiate(RotatorEntity, PosCursor, Rot);
                nameBufferRot++;
                RotatorObject.name = (nameBufferRot).ToString() +"Rot";
                Rotator RotatorComponent = RotatorObject.GetComponent<Rotator>();
                if (buffer % 2 == 0)
                    RotatorCursorX += LittleDistance;
                else
                    RotatorCursorX += BigDistance;
                    
                if(i%2 == 0)
                {

                    //Debug.Log(((i / 2 -1) * Vertical) + (Vertical / 2) + ((l + 2) / 2));
                    //Debug.Log(((i / 2) * Vertical)    + (((l + 1) / 2) + 1));
                    //Debug.Log(((i / 2) * Vertical)    + (Vertical/2)  + ((l + 2) / 2));
                    RotatorComponent.SetHexesToRotator(
                        (((i / 2) * Vertical) + (Vertical / 2) + ((l + 2) / 2)),
                        (((i / 2) * Vertical) + (((l + 1) / 2) + 1)),   
                        (((i / 2 - 1) * Vertical) + (Vertical / 2) + ((l + 2) / 2)));
                }
                else
                {
                    //Debug.Log(((i/2) * Vertical)  +  (((l+1)/2)+1));
                    //Debug.Log(((i/2) * Vertical)  +  (Vertical/2)   + ((l+2)/2)) ;
                    
                    RotatorComponent.SetHexesToRotator(
                        ((i / 2) * Vertical) + (((l + 1) / 2) + 1),
                        ((i / 2) * Vertical) + (Vertical / 2) + ((l + 2) / 2),
                        ((i / 2 + 1) * Vertical) + (((l + 1) / 2) + 1));
                }



                PosCursor.Set(StartingPointOffset.x + RotatorCursorX, StartingPointOffset.y + (i * VerDistance), 0);
                buffer++;
            }
        }
        
    }

    public int GetRotatorAmound()
    {
        return nameBufferRot;
    }
    public int GetHexAmound()
    {
        return nameBufferHex;
    }
    public void CheckAllRotators()
    {

        GridCreater gridCreater = GameObject.Find("Main Camera").GetComponent<GridCreater>();

        for (int i = gridCreater.GetRotatorAmound() + 1; i > 0; i--)
        {
            //Upper  Rotator's PairCheck
            GameObject Rotator = GameObject.Find(i.ToString() + "Rot");
            if (Rotator != null)
            {
                //Debug.Log(i);
                Rotator.GetComponent<Rotator>().PairCheck();
            }
        }


    }
}
