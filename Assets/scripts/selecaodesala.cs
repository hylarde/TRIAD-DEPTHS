using UnityEngine;

public class SelecaoDeSala : MonoBehaviour
{
    public static int salaSelecionada = -1;

    public void EscolherSala(int indice)
    {
        if (indice < 0 || indice > 2)
        {
            Debug.LogWarning("Índice de sala inválido!");
            return;
        }

        salaSelecionada = indice;
        Debug.Log("Sala selecionada: " + GetNomeSala(indice));
    }

    public void EscolherSala1()
    {
        EscolherSala(0);
    }

    public void EscolherSala2()
    {
        EscolherSala(1);
    }

    public void EscolherSala3()
    {
        EscolherSala(2);
    }

    public string GetNomeSala(int indice)
    {
        string[] nomes = { "Sala 1", "Sala 2", "Sala 3" };

        if (indice >= 0 && indice < nomes.Length)
        {
            return nomes[indice];
        }

        return "Sala inválida";
    }
}
