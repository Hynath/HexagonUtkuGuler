using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Hex Hex1;
    public Hex Hex2;
    public Hex Hex3;

    private float startPosX;
    private float startPosY;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
       
        startPosX = Input.mousePosition.x;
        startPosY = Input.mousePosition.y;
    }
    private void OnMouseUp()
    {
        float PosDiffX = Input.mousePosition.x - startPosX;
        float PosDiffY = Input.mousePosition.y - startPosY;

        if(PosDiffX > 50)
        {
            if (PosDiffY > 50)
                RotateLeft();
            else if (PosDiffY < -50)
                RotateRight();
        }
        else if (PosDiffX < -50)
        {
            if (PosDiffY > 50)
                RotateRight();
            else if(PosDiffY < -50)
                RotateLeft();
        }

    }

    public void SetHexesToRotator(int Hex1Num, int Hex2Num, int Hex3Num)
    {
        Hex1 = GameObject.Find(Hex1Num.ToString()).GetComponent<Hex>();
        Hex2 = GameObject.Find(Hex2Num.ToString()).GetComponent<Hex>();
        Hex3 = GameObject.Find(Hex3Num.ToString()).GetComponent<Hex>();
    }

    private void RotateLeft()
    {
        MoveCounter.MoveCount += 1;
        CheckAllBombs();
        Hex.HexInfo Buffer;
        Buffer = Hex1.GetHexInfo();


        Hex1.SetHexInfo(Hex3.GetHexInfo());
        Hex3.SetHexInfo(Hex2.GetHexInfo());
        Hex2.SetHexInfo(Buffer);

        PairCheck();
        CheckAllRotators();
        //CheckCloseRotators();


    }
    private void RotateRight()
    {
        MoveCounter.MoveCount += 1;
        CheckAllBombs();
        Hex.HexInfo Buffer;
        Buffer = Hex3.GetHexInfo();


        Hex3.SetHexInfo(Hex1.GetHexInfo());
        Hex1.SetHexInfo(Hex2.GetHexInfo());
        Hex2.SetHexInfo(Buffer);

        PairCheck();
        CheckAllRotators();
        //CheckCloseRotators();

    }
    
    public void PairCheck()
    {
        if (Hex1.GetHexInfo().HexColor == Hex2.GetHexInfo().HexColor
            && Hex2.GetHexInfo().HexColor == Hex3.GetHexInfo().HexColor
            && Hex3.GetHexInfo().HexColor == Hex1.GetHexInfo().HexColor)
        {
            //Debug.Log(+15);
            ScoreScript.scoreValue += 15;
            ScoreScript.BombScoreValue += 15;

            Hex.HexInfo buffer;
            buffer.HexColor = Hex.Color.Empty;
            buffer.SpriteName = "HexEmpty";
            Hex1.SetHexInfo(buffer);
            Hex2.SetHexInfo(buffer);
            Hex3.SetHexInfo(buffer);

            //wait a second
            GridCreater gridCreater = GameObject.Find("Main Camera").GetComponent<GridCreater>();
            int RotatorNo = int.Parse(this.name.Replace("Rot", ""));
            int ColumnNumber = (((RotatorNo-1) / (gridCreater.Vertical-1)));
            int RowNumber = RotatorNo - ((ColumnNumber ) * (gridCreater.Vertical - 1));
            if (RowNumber == 0)
                RowNumber = gridCreater.Vertical - 1;

            GridCreater.OngoingDropCount += 3;
            ColumnNumber++;
            //Debug.Log(ColumnNumber);
            //Debug.Log(RowNumber);

            if (ColumnNumber % 2 == 0)
            {
                if(RowNumber % 2 == 0)
                {
                    //Debug.Log("SOL");
                    
                    Hex2.Drop1();
                    Hex3.Drop2();
                    Hex1.Drop2();


                }
                else
                {
                    //Debug.Log("SAG");
                    Hex2.Drop1();
                    Hex1.Drop2();
                    Hex3.Drop2();


                }
            }
            else
            {
                if(RowNumber % 2 == 0)
                {
                    //Debug.Log("SAG");
                    Hex2.Drop1();
                    Hex1.Drop2();
                    Hex3.Drop2();

                }
                else
                {
                    //Debug.Log("SOL");
                    Hex2.Drop1();
                    Hex3.Drop2();
                    Hex1.Drop2();

                }
            }

        }
    }

    public void CheckCloseRotators()
    {
        int RotatorNo = int.Parse(this.name.Replace("Rot", ""));
        //Debug.Log(RotatorNo);
        GridCreater gridCreater = GameObject.Find("Main Camera").GetComponent<GridCreater>();


        //Upper  Rotator's PairCheck
        GameObject UpperRotator = GameObject.Find((RotatorNo + gridCreater.Vertical - 1).ToString() + "Rot");
        if (UpperRotator != null)
            UpperRotator.GetComponent<Rotator>().PairCheck();
        //Downer Rotator's PairCheck
        GameObject DownRotator = GameObject.Find((RotatorNo - (gridCreater.Vertical - 1)).ToString() + "Rot");
        if (DownRotator != null)
            DownRotator.GetComponent<Rotator>().PairCheck();
        //Right  Rotator's PairCheck
        GameObject RightRotator = GameObject.Find((RotatorNo + 1).ToString() + "Rot");
        if (RightRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 0)
            RightRotator.GetComponent<Rotator>().PairCheck();
        //Left   Rotator's PairCheck
        GameObject LeftRotator = GameObject.Find((RotatorNo - 1).ToString() + "Rot");
        if (LeftRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 1)
            LeftRotator.GetComponent<Rotator>().PairCheck();



        //UpperRight   Rotator's PairCheck
        GameObject UpperRightRotator = GameObject.Find(((RotatorNo + (gridCreater.Vertical - 1)) + 1).ToString() + "Rot");
        Debug.Log(((RotatorNo + (gridCreater.Vertical - 1)) + 1));
        if (UpperRightRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 0) { }
        UpperRightRotator.GetComponent<Rotator>().PairCheck();

        //UpperLeft   Rotator's PairCheck
        GameObject UpperLeftRotator = GameObject.Find(((RotatorNo + (gridCreater.Vertical - 1)) - 1).ToString() + "Rot");
        if (UpperLeftRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 1)
            UpperLeftRotator.GetComponent<Rotator>().PairCheck();


        //DownerRight Rotator's PairCheck
        GameObject DownRightRotator = GameObject.Find(((RotatorNo - (gridCreater.Vertical - 1)) + 1).ToString() + "Rot");
        if (DownRightRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 0) { }
        DownRightRotator.GetComponent<Rotator>().PairCheck();

        //LeftRight Rotator's PairCheck
        GameObject DownLeftRotator = GameObject.Find(((RotatorNo - (gridCreater.Vertical - 1)) - 1).ToString() + "Rot");
        if (DownRightRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 1)
            DownRightRotator.GetComponent<Rotator>().PairCheck();



        //UpperUpper   Rotator's PairCheck
        GameObject UpperUpperRotator = GameObject.Find(((RotatorNo + ((gridCreater.Vertical - 1) * 2))).ToString() + "Rot");
        if (UpperUpperRotator != null)
            UpperUpperRotator.GetComponent<Rotator>().PairCheck();

        //DownDown   Rotator's PairCheck
        GameObject DownDownRotator = GameObject.Find(((RotatorNo - ((gridCreater.Vertical - 1) * 2))).ToString() + "Rot");
        if (DownDownRotator != null)
            DownDownRotator.GetComponent<Rotator>().PairCheck();

        //UpperUpperRight   Rotator's PairCheck
        GameObject UpperUpperRightRotator = GameObject.Find((((RotatorNo + ((gridCreater.Vertical - 1) * 2)) + 1)).ToString() + "Rot");
        if (UpperUpperRightRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 0)
            UpperUpperRightRotator.GetComponent<Rotator>().PairCheck();

        //UpperUpperLeft   Rotator's PairCheck
        GameObject UpperUpperLeftRotator = GameObject.Find((((RotatorNo + ((gridCreater.Vertical - 1) * 2)) - 1)).ToString() + "Rot");
        if (UpperUpperLeftRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 1)
            UpperUpperLeftRotator.GetComponent<Rotator>().PairCheck();

        //DownDownRight   Rotator's PairCheck
        GameObject DownDownRightRotator = GameObject.Find(((RotatorNo - ((gridCreater.Vertical - 1) * 2)) + 1).ToString() + "Rot");
        if (DownDownRightRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 0)
            DownDownRightRotator.GetComponent<Rotator>().PairCheck();

        //DownDownLeft   Rotator's PairCheck
        GameObject DownDownLeftRotator = GameObject.Find(((RotatorNo - ((gridCreater.Vertical - 1) * 2)) - 1).ToString() + "Rot");
        if (DownDownLeftRotator != null && (RotatorNo % (gridCreater.Vertical - 1)) != 1)
            DownDownLeftRotator.GetComponent<Rotator>().PairCheck();
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

    public void CheckAllBombs()
    {
        GridCreater gridCreater = GameObject.Find("Main Camera").GetComponent<GridCreater>();


        for (int i = 1; i < gridCreater.GetHexAmound()+1; i++)
        {
            Hex CurrentHex = GameObject.Find(i.ToString()).GetComponent<Hex>();
            if(CurrentHex.GetHexType() == Hex.HexType.Bomb)
            {

                CurrentHex.SetBombTick();

            }
        }
    }
   
}
