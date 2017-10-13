using System;
using System.Collections.Generic;
using System.Text;

namespace NNeuronFramework.Algorithm
{
    class MatrixMapper
    {
        private int size;
        private List<Neuron> neurons;
        private Neuron[][] n_array;

        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }

        public MatrixMapper(int size, List<Neuron> neurons)
        {
            this.size = size;
            this.neurons = neurons;

            this.ColumnsCount = this.size;
            this.RowsCount = this.size;

            ConstructNeuronArray(size);
        }

        private void ConstructNeuronArray(int size)
        {
            n_array = new Neuron[size][];

            for (var x = 0; x < size; x++)
            {
                n_array[x] = new Neuron[size];

                for (var y = 0; y < size; y++)
                {
                    n_array[x][y] = this.neurons[x * size + y];
                }
            }
        }

        internal List<SOMNeighbor> ClosestNeurons(Neuron winner, int radius)
        {
            List<SOMNeighbor> results = new List<SOMNeighbor>();

            Tuple<int, int> winnerCoordinate = ParseCoordinate(winner);
            if (winnerCoordinate.Item1 == -1 || winnerCoordinate.Item2 == -1)
                return null;

            for (var distance = 0; distance < radius; distance++)
            {
                List<Neuron> foundedNeurons = FindNeuronsByDistance(winnerCoordinate.Item1, winnerCoordinate.Item2, distance);
                if (foundedNeurons != null && foundedNeurons.Count > 0)
                {
                    SOMNeighbor neighbor = new SOMNeighbor();

                    neighbor.Distance = distance;
                    neighbor.Neurons = foundedNeurons;

                    results.Add(neighbor);
                }
            }

            return results;
        }

        private List<Neuron> FindNeuronsByDistance(int x, int y, int distance)
        {
            if (distance <= 0)
                return new List<Neuron>() { this.n_array[x][y] };

            var results = new List<Neuron>();

            //上
            if (x - distance >= 0)
                results.Add(this.n_array[x - distance][y]);
            //下
            if (x + distance < size)
                results.Add(this.n_array[x + distance][y]);
            //左
            if (y - distance >= 0)
                results.Add(this.n_array[x][y - distance]);
            //右
            if (y + distance < size)
                results.Add(this.n_array[x][y + distance]);

            return results;
        }

        public Neuron GetCell(int x, int y)
        {
            return this.n_array[x][y];
        }

        private Tuple<int, int> ParseCoordinate(Neuron winner)
        {
            for (var x = 0; x < size; x++)
                for (var y = 0; y < size; y++)
                    if (this.n_array[x][y] == winner)
                        return Tuple.Create<int, int>(x, y);

            return Tuple.Create<int, int>(-1, -1);
        }
    }
}
