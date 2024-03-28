using Destiny.GameModules;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Destiny.CoreModules
{


    public class Quadtree<TType> where TType : QuadtreeItem
    {
        /*
        private QuadtreeNode<TType> node;
                
        private int depth;

        public enum EQuadtreeIndex
        {
            TopLeft = 0,        // 00
            TopRight = 1,       // 01
            BottomLeft = 2,     // 10
            BottomRight = 3,    // 11
        }

        public IEnumerable<QuadtreeNode<TType>> GetLeafNodes(RigidBody.Rectangle rectangle)
        {
            return GetRoot().GetLeafNodes(rectangle);
        }

        public void Insert(Vector2f pos, TType value, bool ForseOptimize = true, bool plus1 = true, bool plusTileSize = true)
        {
            Vector2f newpos = pos;

            if (plusTileSize)
                newpos = new Vector2f(pos.X * Tile.tileSize, pos.Y * Tile.tileSize);
            if (plus1)
                newpos += new Vector2f(1, 1);

            TType newval = null;
            if (value != null)
                newval = (TType)value.Clone();

            var leafNode = node.Subdivide(newpos, newval, depth);
            leafNode.Data = value;

            //leafNode.Optimize();

            if (ForseOptimize)
                Optimize();

        }

        public TType Get(Vector2f pos, bool TilePosStyle = true)
        {
            try
            {

                Vector2f position;

                if (TilePosStyle)
                    position = new Vector2f(pos.X * Tile.tileSize, pos.Y * Tile.tileSize) + new Vector2f(1, 1);
                else
                    position = pos;

                if (Physics.CheckCollision(new RigidBody.Rectangle(new Vector2f(0, 0), new Vector2f(node.Size, node.Size)), position))
                {
                    var val = node.Search(position);
                    return val.Data;
                }
                return null;
            }
            catch { return null; }
        }

        public void Optimize()
        {
            for (int i = 0; i < GetRoot().Depth; i++)
                node.Optimize();
        }

        public Quadtree(Vector2f pos, float size, int depth)
        {
            node = new QuadtreeNode<TType>(pos, size, depth);
            //node.Subdivide(depth);
            this.depth = depth;
        }

        public void DrawVisibleNodes(Quadtree<TType>.QuadtreeNode<TType> tree)
        {
            if (tree.Nodes != null)
            {
                foreach (var node in tree.Nodes)
                {
                    if (node.HasNotNullLeaf())
                        if (Physics.CheckCollision(Camera.position, new Vector2f(RenderCore.window.Size.X, RenderCore.window.Size.Y), node.Transform, new Vector2f(node.Size, node.Size)))
                        {
                            if (node.IsLeaf())
                            {
                                RenderCore.DrawCollisionSquare(node.Transform, new Vector2f(node.Size, node.Size), Color.Blue);

                                if (node.Data != null)
                                {
                                    RenderCore.DrawCollisionSquare(node.Transform + new Vector2f(1, 1), new Vector2f(node.Size - 2, node.Size - 2), Color.Red);
                                }
                            }
                            else
                                DrawVisibleNodes(node);
                        }
                }
            }
        }

        public void DrawNodes(Quadtree<TType>.QuadtreeNode<TType> node, int nodeDepth = 0)
        {
            if (!node.IsLeaf())
            {
                if (node.Nodes != null)
                {
                    foreach (var subnode in node.Nodes)
                    {
                        DrawNodes(subnode, nodeDepth + 1);
                    }
                }
            }

            RenderCore.DrawCollisionSquare(node.Transform, new Vector2f(node.Size, node.Size), Color.Blue);
            
            if (node.Data != null)
            {
                RenderCore.DrawCollisionSquare(node.Transform + new Vector2f(1,1), new Vector2f(node.Size-2, node.Size-2), Color.Red);
            }
        }
        /*
        public void DrawNodes(int nodeDepth = 0)
        {
            if (!node.IsLeaf())
            {
                foreach (var subnode in node)
                {
                    DrawNodes(subnode, nodeDepth + 1);
                }
            }
            RenderCore.DrawCollisionSquare(node.Transform, new Vector2f(node.Size, node.Size), Color.Blue);
        }
        */
        /*
        public QuadtreeNode<TType> GetRoot()
        {
            return node;
        }

        public IEnumerable<QuadtreeNode<TType>> GetLeafNodes()
        {
            return node.GetLeafNodes();
        }

        
        public class QuadtreeNode<TType> where TType : QuadtreeItem
        {
            
            Vector2f position;

            
            float size;

            
            QuadtreeNode<TType>[] subNodes;

            
            TType data;

            
            int depth;

            public void ForseSubdivide(Vector2f pos)
            {
                if (depth != 0)
                {
                    GetIndexOfPosition(pos, Transform);
                    ForseSubDivide(this.data);
                    subNodes[0].ForseSubdivide(pos);
                    subNodes[1].ForseSubdivide(pos);
                    subNodes[2].ForseSubdivide(pos);
                    subNodes[3].ForseSubdivide(pos);
                    this.data = null;
                }
            }

            public void ForseSubDivide(TType olddata)
            {
                if (depth != 0)
                {
                    subNodes = new QuadtreeNode<TType>[4];

                    for (int i = 0; i < subNodes.Length; ++i)
                    {
                        Vector2f newPos = position;

                        if (((i & 2) == 2) | ((i & 3) == 3))
                        {
                            newPos.Y += size * 0.5f;
                        }

                        if (((i & 1) == 1) | (((i & 3) == 3)))
                        {
                            newPos.X += size * 0.5f;
                        }

                        subNodes[i] = new QuadtreeNode<TType>(newPos, size * 0.5f, depth - 1);
                        if (olddata != null)
                        subNodes[i].data = (TType)olddata.Clone();
                    }
                }
            }

            public int Depth
            {
                get { return depth; }
            }


            public TType Data
            {
                get { return data; }
                internal set { this.data = value; }
            }

            public QuadtreeNode(Vector2f pos, float size, int depth, TType value = default(TType))
            {
                this.position = pos;
                this.depth = depth;
                this.data = value;
                this.size = size;
            }

            public IEnumerable<QuadtreeNode<TType>> Nodes
            {
                get { return subNodes; }
            }


            public Vector2f Transform
            {
                get { return position; }
            }

            public float Size
            {
                get { return size; }
            }

            public bool HasNotNullLeaf()
            {
                try
                {
                    if (subNodes == null)
                        if (data == null)
                            return false;
                        else
                            return true;

                    if (!IsLeaf())
                        if (subNodes != null)
                        {
                            foreach (var leaf in subNodes)
                            {
                                if (leaf.HasNotNullLeaf())
                                    return true;
                            }
                        }

                    return false;
                }
                catch { return false; }
            }

            public void Optimize()
            {

                if (depth == 0)
                    return;

                if (this.subNodes != null)
                {
                    if ((subNodes[0].data != null) & (subNodes[1].data != null) & (subNodes[2].data != null) & (subNodes[3].data != null))
                    if (subNodes[0].data.Compare(subNodes[1].data) & subNodes[2].data.Compare(subNodes[3].data) & subNodes[0].data.Compare(subNodes[3].data))
                    {
                        this.data = subNodes[0].data;
                        this.subNodes = null;
                        return;
                    }

                    if ((subNodes[0].data == null) & (subNodes[1].data == null) & (subNodes[2].data == null) & (subNodes[3].data == null))
                        if ((subNodes[0].subNodes == null) & (subNodes[1].subNodes == null) & (subNodes[2].subNodes == null) & (subNodes[3].subNodes == null))
                    {
                        this.data = subNodes[0].data;
                        this.subNodes = null;
                        return;
                    }
                }

                if (this.subNodes != null)
                {
                    for (int i = 0; i < subNodes.Length; ++i)
                    {
                        subNodes[i].Optimize();
                    }
                }

            }

            public QuadtreeNode<TType> Search(Vector2f targetPos)
            {
                try
                {
                    if (depth == 0)
                        return this;

                    if (data != null)
                        return this;

                    if (this.subNodes == null)
                        return this;


                    int subDivIndex = GetIndexOfPosition(targetPos, position);

                    return subNodes[subDivIndex].Search(targetPos);
                }
                catch { return null; }
            }

            public QuadtreeNode<TType> Subdivide(Vector2f targetPos, TType value, int depth = 0)
            {
                if (depth == 0)
                {
                    //Optimize();
                    return this;
                }
                
                if ((data != null) | (subNodes == null))
                {
                    ForseSubdivide(targetPos);
                }
                
                int subDivIndex = GetIndexOfPosition(targetPos, position);

                if (subNodes == null)
                {
                    subNodes = new QuadtreeNode<TType>[4];

                    for (int i = 0; i < subNodes.Length; ++i)
                    {
                        Vector2f newPos = position;

                        if (((i & 2) == 2) | ((i & 3) == 3))
                        {
                            newPos.Y += size * 0.5f;
                        }

                        if (((i & 1) == 1) | (((i & 3) == 3)))
                        {
                            newPos.X += size * 0.5f;
                        }

                        subNodes[i] = new QuadtreeNode<TType>(newPos, size * 0.5f, depth - 1);
                        if (depth > 0 && subDivIndex == i)
                        {
                            subNodes[i].Subdivide(targetPos, value, depth - 1);
                        }
                    }
                }

                return subNodes[subDivIndex].Subdivide(targetPos, value, depth - 1);
            }



            public int GetIndexOfPosition(Vector2f lookupPos, Vector2f nodePos)
            {
                int index = 0;

                if (lookupPos.Y > nodePos.Y + this.size / 2)
                {
                    if (lookupPos.X > nodePos.X + this.size / 2)
                        index = 3;
                    else
                        index = 2;
                }
                else
                {
                    if (lookupPos.X > nodePos.X + this.size / 2)
                        index = 1;
                    else index = 0;
                }
                return index;
            }

            public IEnumerable<QuadtreeNode<TType>> GetLeafNodes(RigidBody.Rectangle rectangle)
            {
                if (IsLeaf())
                {
                    if (Physics.CheckCollision(rectangle, new RigidBody.Rectangle(position, size)))
                        yield return this;
                    else
                        yield return null;
                }
                else
                {
                    if (Nodes != null)
                        foreach (var node in Nodes)
                        {
                            foreach (var leaf in node.GetLeafNodes(rectangle))
                            {
                                yield return leaf;
                            }
                        }
                }
            }

            public IEnumerable<QuadtreeNode<TType>> GetLeafNodes()
            {
                if (IsLeaf())
                {
                    yield return this;
                }
                else
                {
                    if (Nodes != null)
                    foreach (var node in Nodes)
                    {
                        foreach (var leaf in node.GetLeafNodes())
                        {
                            yield return leaf;
                        }
                    }
                }
            }

            public bool IsLeaf()
            {
                return ((data != null)|(depth == 0));
            }
        }
        */
    }
}
