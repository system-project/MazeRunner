using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



///1つ1つのノードクラス
public class ACONode
{
    ///最短経路確定状態の列挙体
    public enum NodeStatus
    {
        NotYet,     // 未確定状態
        Temporary,  // 仮確定状態
        Completed   // 確定状態{
    }
    public double Distance = 1.0;             // 距離
    public ACONode SourceNode;     // ソースノード
    public NodeStatus Status;           // 状態
    public int X;                       // ノード位置X
    public int Z;                       // ノード位置Z
    public int nodeNumber;

}

// new DijkstraBranch(node1, node2)
/// 道のつながりを作成
public class ACOBranch
{
    public readonly ACONode Node1; // ノード1
    public readonly ACONode Node2; // ノード2
    public readonly double Distance;    // 距離
    public double pheromone;
    public double p;

    public ACOBranch(ACONode node1, ACONode node2) //new時の 初期化関数
    {
        Node1 = node1;
        Node2 = node2;
        Distance = 1.0;
        pheromone = 0.0;
        p = 0.0;
    }
}

public class Agent
{

    public double sumdistance;
    public ACONode SourceNode;
    public List<ACOBranch> agentbranch; 
    public int agentNumber;
}

/// ダイクストラ法アルゴリズム実装
public class ACOalgo : MonoBehaviour
{
    // ブランチここに入れる
    public List<ACOBranch> Branches
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
    public List<ACONode> Nodes
    {
        get
        {
            return _nodes;
        }
    }
    public List<Agent> Agents
    {
        get
        {
            return _agent;
        }
    }
    
    private List<ACOBranch> _branches;
    private List<ACONode> _nodes;
    private List<Agent> _agent;

    /// コンストラクタ
    /// nNodeCount => 全ノード数
    /// class Dijkstrの初期化関数
    public ACOalgo(int nNodeCount)
    {
        _nodes = new List<ACONode>();
        for (int i = 0; i < nNodeCount; i++)
        {
            var c = new ACONode();
            c.nodeNumber = i;
            _nodes.Add(c);
        }

    }

    public void newagent(int aGentCount)
    {
        
        _agent = new List<Agent>();
        for (int i = 0; i < aGentCount; i++)
        {
            var d = new Agent();
            d.agentNumber = i;
            _agent.Add(d);
            d.agentbranch = new List<ACOBranch>();
        }
    }

        /// 最短経路計算実行
        /// nStart => スタートノードのインデックス
        /// nCount => 検索回数
        public ACONode Execute(ACONode startNode, ACONode goalNode,double alpha,double beta,int SEED,int NumOfSolve, int n)
    {

        int S = 0;
        ACONode ansewr = startNode;
        if (startNode == null) return null;
        foreach (ACOBranch branch in _branches) //pheromoneの初期化
        {
            branch.pheromone = 0.0;
        }
        
        while (S != NumOfSolve)
        {
            newagent(n);
            ansewr = AgentMove(startNode,goalNode,alpha,beta,SEED);
            UpdatePheromone(goalNode);
            
            S++;
        }

            return ansewr;
    }

    private ACONode AgentMove(ACONode startnode, ACONode goalnode,double alpha,double beta,int SEED)
    {
        System.Random rnd = new System.Random(SEED);
        ACONode ansernode = startnode;
        double sumdistance = 0.0;
        double sumpheromone = 0.0;
        double sump = 0.0;

        foreach (Agent agent in _agent)
        {
            agent.SourceNode = startnode;
            sumdistance = 0.0;
            sumpheromone = 0.0;
            sump = 0.0;
            while(agent.SourceNode.nodeNumber != goalnode.nodeNumber && agent.SourceNode.nodeNumber != 18)
            {
                sumdistance = 0.0;
                foreach (ACOBranch branch in _branches)
                {
                    
                    foreach (ACOBranch branch2 in _branches){

                        if (branch2.Node1.Equals(agent.SourceNode) == true)
                        {
                            sumpheromone = sumpheromone + Math.Pow(branch2.pheromone, alpha);
                            sumdistance = sumdistance + Math.Pow(branch2.Distance, beta);
                            print(sumpheromone);
                            print(sumdistance);

                           
                        }
                        else
                        {

                            continue;
                        }
                    }
                    print(branch.p);
                    branch.p = (Math.Pow(branch.pheromone, alpha) + Math.Pow(branch.Distance, beta)) / (sumpheromone + sumdistance);

                }

                foreach (ACOBranch branch in _branches)
                {
                    if (branch.Node1.Equals(agent.SourceNode) == true)
                    {
                        sump = sump + branch.p;
                       // print(branch.p);
                        if (rnd.NextDouble() <= sump)
                        {
                            agent.agentbranch.Add(branch);
                            //print(branch.Node2.nodeNumber);
                            agent.SourceNode = branch.Node2;
                            agent.sumdistance++;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if (agent.SourceNode == goalnode)
                {
                    ansernode = agent.SourceNode;
                }
            }

            
        }

            return ansernode;
        
        
        
    }

    private void UpdatePheromone(ACONode goal)
    {
        foreach(Agent agent in _agent)
        {
            if(agent.SourceNode == goal)
            {
                foreach (ACOBranch branch in _branches)
                {

                    if (agent.agentbranch.Contains(branch))
                    {
                        branch.pheromone = 1 / agent.sumdistance;
                    }
                }
            }
            
        }
    }



    /// 指定ノードに隣接するノードの最短距離を計算する
    /// 関数で使う関数
    /// sourceNode => 指定ノード
    

}

public class ACO : MonoBehaviour
{

    public int count = 19;
    public int agentnum = 10;
    public int solve = 10;
    public int a = 1;
    public int b = 1;
    public int seed = 300;
    // info[0][1] == 0
    public int[][] info = {
        new int[]{ 0, 1 },
        new int[]{ 1, 2 },
        new int[]{ 2, 3 },
        new int[]{ 3,18},
        new int[]{ 2, 6 },
        new int[]{ 1, 5 },
        new int[]{ 4, 5 },
        new int[]{ 5, 6 },
        new int[]{ 5, 9 },
        new int[]{ 6, 7 },
        new int[]{ 7, 18},
        new int[]{ 4, 8 },
        new int[]{ 8, 12 },
        new int[]{ 6, 10 },
        new int[]{ 9, 10 },
        new int[]{ 11,18},
        new int[]{ 10, 14 },
        new int[]{ 12, 13 },
        new int[]{ 13, 14 },
        new int[]{ 14, 15 },
        new int[]{ 15, 11 }
    };

    public ACOalgo aco;
    public List<ACOBranch> branches;
    public ACONode answerNode;

    // Use this for initialization
    void Start()
    {
        //初期化
        aco = new ACOalgo(count);
        branches = new List<ACOBranch>();
        foreach (int[] bond in info)
        {
            branches.Add(new ACOBranch(aco.Nodes[bond[0]], aco.Nodes[bond[1]]));
        }

        //breanch入れていく
        aco.Branches = branches;

        answerNode = aco.Execute(aco.Nodes[0], aco.Nodes[12],a,b,seed,solve,agentnum);

        print(answerNode.nodeNumber);
    }


    // Update is called once per frame
    void Update()
    {

        
    }

}
