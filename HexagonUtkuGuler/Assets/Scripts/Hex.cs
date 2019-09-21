using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Hex : MonoBehaviour
{
   public enum Color
{
    Red,
    Blue,
    Green,
    Yellow,
    Orange,
    Empty
}
    public enum HexType
    {
        Normal,
        Bomb
    }

    public struct HexInfo
    {
        public Hex.Color HexColor ;
        public string SpriteName;
    }

    private HexInfo MyHexInfo;
    private HexType MyHexType;
    private int BombFuseTime;


    // Start is called before the first frame update
    void Start()
    {
        BombFuseTime = 5;
        RandomHexGenerate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RandomHexGenerate()
    {
        MyHexType = HexType.Normal;
        int Number = Random.Range(1, 6);
        if(Number == 1)
        {

            MyHexInfo.HexColor = Color.Red;
            MyHexInfo.SpriteName = "HexRed";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if(Number == 2)
        {
            MyHexInfo.HexColor = Color.Blue;
            MyHexInfo.SpriteName = "HexBlue";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if (Number == 3)
        {
            MyHexInfo.HexColor = Color.Green;
            MyHexInfo.SpriteName = "HexGreen";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if (Number == 4)
        {
            MyHexInfo.HexColor = Color.Yellow;
            MyHexInfo.SpriteName = "HexYellow";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if (Number == 5)
        {
            MyHexInfo.HexColor = Color.Orange;
            MyHexInfo.SpriteName = "HexOrange";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
    }
    void RandomBombGenerate()
    {
        MyHexType = HexType.Bomb;
        int Number = Random.Range(1, 6);
        if (Number == 1)
        {
            MyHexInfo.HexColor = Color.Red;
            MyHexInfo.SpriteName = "RedBomb5";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if (Number == 2)
        {
            MyHexInfo.HexColor = Color.Blue;
            MyHexInfo.SpriteName = "BlueBomb5";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if (Number == 3)
        {
            MyHexInfo.HexColor = Color.Green;
            MyHexInfo.SpriteName = "GreenBomb5";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if (Number == 4)
        {
            MyHexInfo.HexColor = Color.Yellow;
            MyHexInfo.SpriteName = "YellowBomb5";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else if (Number == 5)
        {
            MyHexInfo.HexColor = Color.Orange;
            MyHexInfo.SpriteName = "OrangeBomb5";
            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
    }

    public HexInfo GetHexInfo()
    {
        return MyHexInfo;
    }
    public HexType GetHexType()
    {
        return MyHexType;
    }
    public void SetHexInfo(HexInfo info)
    {
        MyHexInfo.HexColor = info.HexColor;
        MyHexInfo.SpriteName = info.SpriteName;
        Sprite MySprite = Resources.Load<Sprite>(info.SpriteName);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
    }
    public void SetHexType(HexType type)
    {
        MyHexType = type;

    }

    public void Drop1()
    {
        GridCreater gridCreater = GameObject.Find("Main Camera").GetComponent<GridCreater>();
        int HexNo = int.Parse(this.name);
        //Debug.Log(HexNo);
        //Debug.Log(HexNo + gridCreater.Vertical);
        //Debug.Log(gridCreater.GetHexAmound());
        if (HexNo + gridCreater.Vertical <= gridCreater.GetHexAmound())
        {
            Hex UpperHex = GameObject.Find((HexNo + gridCreater.Vertical).ToString()).GetComponent<Hex>();
            if (UpperHex.GetHexInfo().HexColor != Hex.Color.Empty)
            {
                if(UpperHex.MyHexType == HexType.Bomb)
                {
                    UpperHex.SetHexType(HexType.Normal);
                    MyHexType = HexType.Bomb;
                    BombFuseTime = UpperHex.BombFuseTime;
                    UpperHex.BombFuseTime = 10;
                }
                SetHexInfo(UpperHex.GetHexInfo());
                UpperHex.Drop1();
            }
            else
            {
                UpperHex.Drop1();
                Drop1();
            }
                
        }
        else
        {
            if (ScoreScript.BombScoreValue > 1000)
            {
                RandomBombGenerate();
                ScoreScript.BombScoreValue -= 1000;
            }
            else
            {
                RandomHexGenerate();
            }
            GridCreater.OngoingDropCount -= 1;
            if(GridCreater.OngoingDropCount == 0)
            {
                CheckAllRotators();
            }    
        }
    }
    public void Drop2()
    {

        GridCreater gridCreater = GameObject.Find("Main Camera").GetComponent<GridCreater>();
        int HexNo = int.Parse(this.name);
        //Debug.Log(HexNo);
        //Debug.Log(HexNo + gridCreater.Vertical);
        //Debug.Log(gridCreater.GetHexAmound());
        if (HexNo + (gridCreater.Vertical*2) <= gridCreater.GetHexAmound())
        {
            Hex UpperHex = GameObject.Find((HexNo + (gridCreater.Vertical*2)).ToString()).GetComponent<Hex>();

            if (UpperHex.GetHexInfo().HexColor != Hex.Color.Empty)
            {
                if (UpperHex.MyHexType == HexType.Bomb)
                {
                    UpperHex.SetHexType(HexType.Normal);
                    MyHexType = HexType.Bomb;
                    BombFuseTime = UpperHex.BombFuseTime;
                    UpperHex.BombFuseTime = 10;
                }
                SetHexInfo(UpperHex.GetHexInfo());
                UpperHex.Drop2();
            }
            else
            {
                UpperHex.Drop2();
                Drop2();
            }
        }
        else
        {
            if(ScoreScript.BombScoreValue > 1000)
            {
                RandomBombGenerate();
                ScoreScript.BombScoreValue -= 1000;
            }
            else
            {
                RandomHexGenerate();
            }
            
            GridCreater.OngoingDropCount -= 1;
            if(GridCreater.OngoingDropCount == 0)
            {
                CheckAllRotators();
            }
            
            
        }
    }
    public void CheckAllRotators()
    {

        GridCreater gridCreater = GameObject.Find("Main Camera").GetComponent<GridCreater>();

        for (int i = gridCreater.GetRotatorAmound() + 1; i > 0 ; i--)
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

    public void SetBombTick()
    {
        if(BombFuseTime > 1)
        {
            MyHexInfo.SpriteName = MyHexInfo.SpriteName.Replace(BombFuseTime.ToString(), (BombFuseTime - 1).ToString());
            BombFuseTime--;

            Sprite MySprite = Resources.Load<Sprite>(MyHexInfo.SpriteName);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = MySprite;
        }
        else
        {
            GameObject GameOverImage = GameObject.Find("Image");
            GameOverManager GameOverManager = GameOverImage.GetComponent<GameOverManager>();
            GameOverManager.StartGameOverAnimation();
            Debug.Log("GAME OVER");
        }
        
        
    }
}
