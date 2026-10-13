using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum TipoInimigo
{
    MOSNTRO,
    CREEPER,
    SLIME,
    OVNI,
    LOBISOMEN

}

public class Enum : MonoBehaviour
{
   public TipoInimigo tipo; 

   void Star()
   {
    print(tipo);
   }
}
