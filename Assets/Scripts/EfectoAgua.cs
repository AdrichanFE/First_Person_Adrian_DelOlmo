using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EfectoAgua : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private Volume efecto;
    // Start is called before the first frame update
    void Start()
    {
        efecto=GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log(Mathf.Cos(velocidad*Time.time));


        if(efecto.profile.TryGet(out LensDistortion distorsion))
        {
            FloatParameter xValue = new FloatParameter(1+Mathf.Cos(velocidad * Time.time)/2);
            FloatParameter yValue = new FloatParameter(1+Mathf.Sin(velocidad * Time.time)/2);
            distorsion.xMultiplier.SetValue(xValue);
            distorsion.yMultiplier.SetValue(yValue);
        }
        
    }
}
