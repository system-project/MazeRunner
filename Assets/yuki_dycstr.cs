using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


///1つ1つのノードクラス
public class DijkstraNode
{
    ///最短経路確定状態の列挙体
    public enum NodeStatus
    {
        NotYet,     // 未確定状態
        Temporary,  // 仮確定状態
        Completed   // 確定状態
    }

    public double Distance = 1.0;       // 距離
    public DijkstraNode SourceNode;     // ソースノード
    public NodeStatus Status;           // 状態
    public int X;                       // ノード位置X
    public int Z;                       // ノード位置Z
    public int nodeNumber;

}

// new DijkstraBranch(node1, node2)
/// 道のつながりを作成
public class DijkstraBranch
{
    public readonly DijkstraNode Node1; // ノード1
    public readonly DijkstraNode Node2; // ノード2
    public readonly double Distance;    // 距離

    public DijkstraBranch(DijkstraNode node1, DijkstraNode node2) //new時の 初期化関数
    {
        Node1 = node1;
        Node2 = node2;
        Distance = 1.0;
    }
}

/// ダイクストラ法アルゴリズム実装
public class Dijkstra: MonoBehaviour
{
    // ブランチここに入れる
    public List<DijkstraBranch> Branches
    {
        get
        {
            return _branches;
        }
        set
        {
            _branches = value;
        }
    }
    // ノード
    public List<DijkstraNode> Nodes
    {
        get
        {
            return _nodes;
        }
    }

    private List<DijkstraBranch> _branches;
    private List<DijkstraNode> _nodes;

    /// コンストラクタ
    /// nNodeCount => 全ノード数
    /// class Dijkstrの初期化関数
    public Dijkstra()
    {
        _nodes = new List<DijkstraNode>();
        for (int i = 0; i < 16; i++)      //////ここにnodeの個数を入れる
        {
            var c = new DijkstraNode
            {
                nodeNumber = i
            };
            _nodes.Add(c);
        }
    }

    /// 最短経路計算実行
    /// nStart => スタートノードのインデックス
    /// nCount => 検索回数
    public IEnumerator Execute(DijkstraNode startNode, DijkstraNode goalNode, Action<DijkstraNode> ReturnFunction)
    {
        if (startNode == null) yield return null;
        Color water = new Color(0.1f, 1f, 1f);
        Color black = new Color(0.1f, 0.1f, 0.1f);

        // 全節点で距離を無限大，未確定とする
        foreach (DijkstraNode node in _nodes)
        {
            node.Distance = int.MaxValue;
            node.Status = DijkstraNode.NodeStatus.NotYet;
            node.SourceNode = null;
        }

        // 始点では距離はゼロ，確定とする
        startNode.Distance = 0;
        startNode.Status = DijkstraNode.NodeStatus.Completed;
        startNode.SourceNode = null;

        DijkstraNode sourceNode = startNode;
        GameObject startBlock = GameObject.Find(startNode.nodeNumber.ToString());
        startBlock.GetComponent<Renderer>().material.SetColor("_Color", water);

        yield return new WaitForSeconds(1);

        while (sourceNode != null)
        {
            //UpdateNodeProp(scanNode);       // 隣接点のノードを更新

            if (_branches == null) continue;

            DijkstraNode destinationNode;
            double dTotalDistance;


            // ブランチリストの中から指定ノードに関連しているものを検索
            foreach (DijkstraBranch branch in _branches)
            {

                destinationNode = null;

                if (branch.Node1.Equals(sourceNode) == true)
                {
                    destinationNode = branch.Node2;
                }
                else if (branch.Node2.Equals(sourceNode) == true)
                {
                    destinationNode = branch.Node1;
                }
                else
                {
                    continue;
                }
                // 確定しているノードは無視。
                if (destinationNode.Status == DijkstraNode.NodeStatus.Completed)
                {
                    continue;
                }
                // 隣接ノードを見つけた。
                print("検索Node:" + destinationNode.nodeNumber);

                GameObject block = GameObject.Find(destinationNode.nodeNumber.ToString());
                block.GetComponent<Renderer>().material.SetColor("_Color", water);

                yield return new WaitForSeconds(1);

                // ノードの現在の距離に枝の距離を加える。
                dTotalDistance = sourceNode.Distance + branch.Distance;

                if (destinationNode.Distance <= dTotalDistance) continue;

                // 現在の仮の最短距離よりもっと短い行き方を見つけた。
                destinationNode.Distance = dTotalDistance;  // 仮の最短距離
                destinationNode.SourceNode = sourceNode;
                destinationNode.Status = DijkstraNode.NodeStatus.Temporary;
            }

            sourceNode = FindMinNode();       // 最短経路をもつノードを検索

            if (sourceNode.nodeNumber == goalNode.nodeNumber)
            {
                print("goalしました");
                break;
            }
        }

        ReturnFunction(sourceNode);
    }

    /// 指定ノードに隣接するノードの最短距離を計算する
    /// 関数で使う関数
    /// sourceNode => 指定ノード
    private void UpdateNodeProp(DijkstraNode sourceNode)
    {
        if (_branches == null) return;

        DijkstraNode destinationNode;
        double dTotalDistance;

        // ブランチリストの中から指定ノードに関連しているものを検索
        foreach (DijkstraBranch branch in _branches)
        {
            destinationNode = null;
            if (branch.Node1.Equals(sourceNode) == true)
            {
                destinationNode = branch.Node2;
            }
            else if (branch.Node2.Equals(sourceNode) == true)
            {
                destinationNode = branch.Node1;
            }
            else
            {
                continue;
            }
            // 確定しているノードは無視。
            if (destinationNode.Status == DijkstraNode.NodeStatus.Completed) continue;
            // 隣接ノードを見つけた。
            print("検索Node:" + destinationNode.nodeNumber);

            // ノードの現在の距離に枝の距離を加える。
            dTotalDistance = sourceNode.Distance + branch.Distance;

            if (destinationNode.Distance <= dTotalDistance) continue;

            // 現在の仮の最短距離よりもっと短い行き方を見つけた。
            destinationNode.Distance = dTotalDistance;  // 仮の最短距離
            destinationNode.SourceNode = sourceNode;
            destinationNode.Status = DijkstraNode.NodeStatus.Temporary;
        }
    }

    /// 未確定ノードの中で最短経路をもつノードを検索
    /// 関数で使う関数
    /// return 最短経路をもつノード
    private DijkstraNode FindMinNode()
    {
        double dMinDistance = int.MaxValue; // 最小値を最初無限大とする

        DijkstraNode Finder = null;

        // 全てのノードをチェック
        foreach (DijkstraNode node in _nodes)
        {
            // 確定したノードは無視
            if (node.Status == DijkstraNode.NodeStatus.Completed) continue;

            // 未確定のノードの中で最短距離のノードを探す
            if (node.Distance >= dMinDistance) continue;

            dMinDistance = node.Distance;
            Finder = node;
        }
        if (Finder == null) return null;

        // 最短距離を見つけた。このノードは確定！
        Finder.Status = DijkstraNode.NodeStatus.Completed;
        return Finder;

    }

}

public class yuki_dycstr : MonoBehaviour {
    // info[0][1] == 0
    public int[][] info = {
        new int[]{ 0, 1 },
        new int[]{ 1, 2 },
        new int[]{ 2, 3 },
        new int[]{ 2, 6 },
        new int[]{ 1, 5 },
        new int[]{ 4, 5 },
        new int[]{ 5, 6 },
        new int[]{ 5, 9 },
        new int[]{ 6, 7 },
        new int[]{ 4, 8 },
        new int[]{ 8, 12 },
        new int[]{ 6, 10 },
        new int[]{ 9, 10 },
        new int[]{ 10, 14 },
        new int[]{ 12, 13 },
        new int[]{ 13, 14 },
        new int[]{ 14, 15 },
        new int[]{ 15, 11 }
    };

    public Dijkstra dijkstra;
    public List<DijkstraBranch> branches;
    public DijkstraNode answerNode;

    // Use this for initialization
    void Start () {
        //初期
        dijkstra = gameObject.AddComponent<Dijkstra>();
        branches = new List<DijkstraBranch>();

        foreach (int[] bond in info)
        {
            branches.Add(new DijkstraBranch(dijkstra.Nodes[bond[0]], dijkstra.Nodes[bond[1]]));
        }

        //breanch入れていく
        dijkstra.Branches = branches;

        StartCoroutine(dijkstra.Execute(dijkstra.Nodes[0], dijkstra.Nodes[12], r => answerNode = r)); 

    }
    int[] numbers = new int[1];
    bool flag = false;

    // Update is called once per frame
    void Update () {
        while (answerNode != null)
        {
            flag = true;
            Array.Resize(ref numbers, numbers.Length + 1);
            numbers[numbers.Length - 1] = answerNode.nodeNumber;
            answerNode = answerNode.SourceNode;
        }

        if (flag)
        {
            foreach (int number in numbers)
            {
                GameObject block = GameObject.Find(number.ToString());
                block.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f));
            }

        }

    }

}