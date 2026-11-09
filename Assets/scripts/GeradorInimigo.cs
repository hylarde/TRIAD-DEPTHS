using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigo : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform jogador;
    [SerializeField] private GameObject prefabInimigo;

    [Header("Geração")]
    [SerializeField] private float intervaloGeracao = 2f;
    [SerializeField] private int quantidadeMaxima = 100;
    [SerializeField] private bool gerarAutomaticamente = true;
    [SerializeField] private string tagInimigo = "Slime";

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

    private Coroutine geracaoCoroutine;

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
            IniciarGeracao();
        }
    }

    private void OnEnable()
    {
        if (gerarAutomaticamente && geracaoCoroutine == null)
        {
            IniciarGeracao();
        }
    }

    private void OnDisable()
    {
        if (geracaoCoroutine != null)
        {
            StopCoroutine(geracaoCoroutine);
            geracaoCoroutine = null;
        }
    }

    private void IniciarGeracao()
    {
        if (geracaoCoroutine != null)
        {
            return;
        }

        geracaoCoroutine = StartCoroutine(GerarInimigosContinuamente());
    }

    private IEnumerator GerarInimigosContinuamente()
    {
        while (true)
        {
            LimparListaDeInimigos();

            if (QuantidadeDeInimigosAtivos() < quantidadeMaxima)
            {
                GerarUmInimigo();
            }

            if (intervaloGeracao <= 0f)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(intervaloGeracao);
            }
        }
    }

    public void GerarUmInimigo()
    {
        if (jogador == null || prefabInimigo == null)
        {
            Debug.LogWarning(
                "GeradorInimigos2D: jogador ou prefab não configurado."
            );

            return;
        }

        if (quantidadeMaxima <= 0)
        {
            return;
        }

        if (QuantidadeDeInimigosAtivos() >= quantidadeMaxima)
        {
            return;
        }

        for (int tentativa = 0;
             tentativa < tentativasPorGeracao;
             tentativa++)
        {
            Vector2 posicaoNascimento =
                CalcularPosicaoEmUmaDasOitoDirecoes();

            if (PosicaoPodeSerUsada(posicaoNascimento))
            {
                GameObject novoInimigo = Instantiate(
                    prefabInimigo,
                    posicaoNascimento,
                    Quaternion.identity
                );

                inimigosAtivos.Add(novoInimigo);
                return;
            }
        }
    }

    private Vector2 CalcularPosicaoEmUmaDasOitoDirecoes()
    {
        Vector2[] direcoes =
        {
            new Vector2(0f, 1f),                    // Norte
            new Vector2(1f, 1f).normalized,         // Nordeste
            new Vector2(1f, 0f),                    // Leste
            new Vector2(1f, -1f).normalized,        // Sudeste
            new Vector2(0f, -1f),                   // Sul
            new Vector2(-1f, -1f).normalized,       // Sudoeste
            new Vector2(-1f, 0f),                  // Oeste
            new Vector2(-1f, 1f).normalized        // Noroeste
        };

        int indiceDirecao = Random.Range(0, direcoes.Length);

        Vector2 direcao = direcoes[indiceDirecao];

        // Permite uma pequena variação dentro da direção escolhida.
        float variacaoAngular = Random.Range(-22.5f, 22.5f);

        direcao = RotacionarVetor(direcao, variacaoAngular);

        float distancia = Random.Range(
            distanciaMinima,
            distanciaMaxima
        );

        return (Vector2)jogador.position + direcao * distancia;
    }

    private Vector2 RotacionarVetor(Vector2 vetor, float angulo)
    {
        float radianos = angulo * Mathf.Deg2Rad;

        float seno = Mathf.Sin(radianos);
        float cosseno = Mathf.Cos(radianos);

        return new Vector2(
            vetor.x * cosseno - vetor.y * seno,
            vetor.x * seno + vetor.y * cosseno
        );
    }

    private bool PosicaoPodeSerUsada(Vector2 posicao)
    {
        const float raioVerificacao = 0.4f;

        bool existeObstaculo = Physics2D.OverlapCircle(
            posicao,
            raioVerificacao,
            camadaObstaculo
        );

        bool existeJogador = Physics2D.OverlapCircle(
            posicao,
            raioVerificacao,
            camadaJogador
        );

        return !existeObstaculo && !existeJogador;
    }

    private int QuantidadeDeInimigosAtivos()
    {
        LimparListaDeInimigos();

        int quantidadeNaLista = inimigosAtivos.Count;

        if (!string.IsNullOrWhiteSpace(tagInimigo))
        {
            int quantidadeNaCena = GameObject
                .FindGameObjectsWithTag(tagInimigo).Length;

            return Mathf.Max(quantidadeNaLista, quantidadeNaCena);
        }

        return quantidadeNaLista;
    }

    private void LimparListaDeInimigos()
    {
        inimigosAtivos.RemoveAll(inimigo => inimigo == null);
    }

    private void OnDrawGizmosSelected()
    {
        if (jogador == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            jogador.position,
            distanciaMinima
        );

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            jogador.position,
            distanciaMaxima
        );
    }
}
