using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorDemo : MonoBehaviour
{
    public float Number1;
    public float Number2;
    public float Screen;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Screen = Add(Number1, Number2);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Screen = Subtract(Number1, Number2);
        }

        
        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            Screen = Multiply(Number1, Number2);
        }
        

        if (Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            Screen = Divide(Number1, Number2);
        }
    }

    public float Add(float number1, float number2)
    {
        float result = number1 + number2;
        return result;
    }

    public float Subtract(float number1, float number2)
    {
        float result = number1 - number2;
        return result;
    }

    public float Multiply(float number1, float number2)
    {
        float result = number1 * number2;
        return result;
    }

    public float Divide(float number1, float number2)
    {
        float result = number1 / number2;
        return result;
    }

}
