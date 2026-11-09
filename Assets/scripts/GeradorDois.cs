using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDois : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform jogador;
    [SerializeField] private GameObject prefabInimigo;

    [Header("Geração")]
    [SerializeField] private float intervaloGeracao = 3f;
    [SerializeField] private int quantidadeMaxima = 20;
    [SerializeField] private bool gerarAutomaticamente = true;

    [Header("Distância do jogador")]
    [SerializeField] private float distanciaMinima = 10f;
    [SerializeField] private float distanciaMaxima = 14f;

    [Header("Tentativas")]
    [SerializeField] private int tentativasPorGeracao = 10;

    [Header("Colisões")]
    [SerializeField] private LayerMask camadaObstaculo;
    [SerializeField] private LayerMask camadaJogador;

    private readonly List<GameObject> inimigosAtivos =
        new List<GameObject>();

    private Coroutine rotinaGeracao;

    private void Start()
    {
        if (jogador == null)
        {
            GameObject objetoJogador =
                GameObject.FindGameObjectWithTag("Player");

            if (objetoJogador != null)
            {
                jogador = objetoJogador.transform;
            }
        }

        if (gerarAutomaticamente)
        {
            rotinaGeracao = StartCoroutine(
                GerarInimigosContinuamente()
            );
        }
    }

    private IEnumerator GerarInimigosContinuamente()
    {
        // Espera antes de gerar o primeiro inimigo.
        yield return new WaitForSeconds(2f);

        while (gerarAutomaticamente)
        {
            LimparListaDeInimigos();

            if (inimigosAtivos.Count < quantidadeMaxima)
            {
                GerarUmInimigo();
            }

            yield return new WaitForSeconds(intervaloGeracao);
        }
    }

    private void GerarUmInimigo()
    {
        if (jogador == null || prefabInimigo == null)
        {
            return;
        }

        if (inimigosAtivos.Count >= quantidadeMaxima)
        {
            return;
        }

        for (int tentativa = 0;
             tentativa < tentativasPorGeracao;
             tentativa++)
        {
            Vector2 posicao =
                CalcularPosicaoEmUmaDasOitoDirecoes();

            if (!PosicaoPodeSerUsada(posicao))
            {
                continue;
            }

            GameObject novoInimigo = Instantiate(
                prefabInimigo,
                posicao,
                Quaternion.identity
            );

            inimigosAtivos.Add(novoInimigo);
            return;
        }
    }

    private Vector2 CalcularPosicaoEmUmaDasOitoDirecoes()
    {
        Vector2[] direcoes =
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f).normalized,
            new Vector2(1f, 0f),
            new Vector2(1f, -1f).normalized,
            new Vector2(0f, -1f),
            new Vector2(-1f, -1f).normalized,
            new Vector2(-1f, 0f),
            new Vector2(-1f, 1f).normalized
        };

        Vector2 direcao =
            direcoes[Random.Range(0, direcoes.Length)];

        float distancia = Random.Range(
            distanciaMinima,
            distanciaMaxima
        );

        return (Vector2)jogador.position
             + direcao * distancia;
    }

    private bool PosicaoPodeSerUsada(Vector2 posicao)
    {
        const float raioVerificacao = 0.4f;

        bool temObstaculo = Physics2D.OverlapCircle(
            posicao,
            raioVerificacao,
            camadaObstaculo
        );

        bool temJogador = Physics2D.OverlapCircle(
            posicao,
            raioVerificacao,
            camadaJogador
        );

        return !temObstaculo && !temJogador;
    }

    private void LimparListaDeInimigos()
    {
        inimigosAtivos.RemoveAll(
            inimigo => inimigo == null
        );
    }

    public void PararGeracao()
    {
        gerarAutomaticamente = false;

        if (rotinaGeracao != null)
        {
            StopCoroutine(rotinaGeracao);
            rotinaGeracao = null;
        }
    }

    public void ContinuarGeracao()
    {
        if (gerarAutomaticamente)
        {
            return;
        }

        gerarAutomaticamente = true;

        rotinaGeracao = StartCoroutine(
            GerarInimigosContinuamente()
        );
    }
}
