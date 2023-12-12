using UnityEngine;

public class Rope : MonoBehaviour
{
    public int nodeCount = 10;
    public float nodeMass = 1f;
    public float segmentLength = 1f;
    public float gravity = 9.8f;
    public float initialDamping = 0.5f;
    public float maxDamping = 0.5f;
    public float dampingIncreaseRate = 0.05f;
    public float tension = 0.3f;
    public float lineWidth = 0.1f;

    private struct Node
    {
        public float mass;
        public Vector3 position;
        public Vector3 velocity;
        public float damping;

        public Node(float mass, Vector3 position, float damping)
        {
            this.mass = mass;
            this.position = position;
            this.damping = damping;
            this.velocity = Vector3.zero;
        }
    }

    private Node[] nodes;
    private LineRenderer lineRenderer;

    void Start()
    {
        InitializeRope();
        CreateLineRenderer();
    }

    void InitializeRope()
    {
        nodes = new Node[nodeCount];
        var firstY = 2f;
        var lastY = 0f;
        var length = nodeCount * segmentLength * 0.5f;
        var x = -length;
        for (var i = 0; i < nodeCount; i++)
        {
            x += segmentLength;
            var y = Mathf.Lerp(firstY, lastY, (float)i / nodeCount);
            var damping = Mathf.Lerp(initialDamping, maxDamping, (float)i / (nodeCount - 1));
            nodes[i] = new Node(nodeMass, new Vector3(x, y, 0f), damping);
        }
    }

    void CreateLineRenderer()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = nodeCount;

        for (var i = 0; i < nodeCount; i++)
        {
            lineRenderer.SetPosition(i, nodes[i].position);
        }
    }

    void Update()
    {
        SimulateRope();
        UpdateLineRenderer();
    }

    void SimulateRope()
    {
        for (var i = 1; i < nodeCount - 1; i++)
        {
            nodes[i].velocity += new Vector3(0f, -gravity * Time.deltaTime, 0f);

            var tensionForceLeft = TensionForce(nodes[i], nodes[i - 1]);
            var tensionForceRight = TensionForce(nodes[i], nodes[i + 1]);

            nodes[i].velocity += tensionForceLeft / nodes[i].mass;
            nodes[i].velocity += tensionForceRight / nodes[i].mass;

            nodes[i].velocity *= 1 - nodes[i].damping * Time.deltaTime;
        }

        for (var i = 1; i < nodeCount - 1; i++)
        {
            nodes[i].position += nodes[i].velocity * Time.deltaTime;
        }
        
        for (var i = 1; i < nodeCount - 1; i++)
        {
            nodes[i].damping = Mathf.Min(nodes[i].damping + dampingIncreaseRate * Time.deltaTime, maxDamping);
        }
    }

    void UpdateLineRenderer()
    {
        for (var i = 0; i < nodeCount; i++)
        {
            lineRenderer.SetPosition(i, nodes[i].position);
        }
    }

    Vector3 TensionForce(Node nodeA, Node nodeB)
    {
        var direction = nodeB.position - nodeA.position;
        var distance = direction.magnitude;
        var tensionForceMagnitude = (distance - segmentLength) * Mathf.Pow(nodeA.mass * nodeB.mass, 0.5f) * tension;
        var tensionForce = tensionForceMagnitude * direction.normalized;
        return tensionForce;
    }
}
