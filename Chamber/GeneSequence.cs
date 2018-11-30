using System;
using System.Collections.Generic;

namespace Chamber
{
	public class GeneSequence
	{
		private Random Random { get; set; }

		/// <summary>
		/// Beginning of this GeneSequence
		/// </summary>
		public Gene Start { get; private set; }

		/// <summary>
		/// The last Gene in the main GeneSequence
		/// </summary>
		private Gene End { get; set; }

		public GeneSequence(char start, Random random = null) : this(new Gene(start), random)
		{
			
		}

		private GeneSequence(Gene start, Random random = null)
		{
			if (random == null)
			{
				random = new Random();
			}

			Random = random;
			Start = start;
			End = Start;
		}


		public GeneSequence Clone()
		{
			return new GeneSequence(Clone(Start));
		}

		/// <summary>
		/// Clones a Gene recursively.
		/// </summary>
		/// <param name="gene"></param>
		/// <returns></returns>
		private Gene Clone(Gene gene)
		{
			Gene clone = new Gene(gene.Value);

			foreach (Gene g in gene.Genes)
			{
				clone.Genes.Add(Clone(g));
			}

			return clone;
		}

		/// <summary>
		/// Creates a copy of this GeneSequence
		/// </summary>
		/// <returns></returns>
		public GeneSequence Copy(double mutateChance = 0.01, double splitChance = 0.002)
		{
			GeneSequence result = new GeneSequence(Clone(Start), Random);

			Queue<Gene> queue = new Queue<Gene>();
			queue.Enqueue(result.Start);

			while (queue.Count > 0)
			{
				Gene current = queue.Dequeue();

				if (Random.NextDouble() < splitChance)
				{
					current.Genes.Add(Gene.Random(Random));
				}

				foreach (Gene gene in current.Genes)
				{
					queue.Enqueue(gene);
				}

				if (Random.NextDouble() < mutateChance)
				{
					current.Value = (char)((current.Value + (Random.Next(2) == 0 ? -1 : 1) + char.MaxValue) % char.MaxValue);
				}
			}

			return result;
		}

		public void Extend(char? newGene)
		{
			Extend(new Gene(newGene ?? (char)Random.Next(char.MaxValue)));
		}

		public void Extend(Gene newGene)
		{
			End.Genes.Add(newGene);
			End = newGene;
		}

		public override string ToString()
		{
			return Start.ToString();
		}

		public override int GetHashCode()
		{
			return Start.GetHashCode();
		}
	}
}
