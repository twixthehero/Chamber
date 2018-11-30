using System;
using System.Collections.Generic;
using System.Text;

namespace Chamber
{
	public class Gene
	{
		public static Gene Random(Random random) =>	new Gene((char)random.Next(char.MaxValue));

		public char Value { get; set; }

		public List<Gene> Genes { get; } = new List<Gene>(2);

		public Gene(char value)
		{
			Value = value;
		}

		public string ToStringFormatted(int level = 0)
		{
			StringBuilder s = new StringBuilder();
			for (int i = 0; i < level; i++)
			{
				s.Append("  ");
			}

			s.Append($"({Value}");

			foreach (Gene gene in Genes)
			{
				s.Append($"\n{gene.ToStringFormatted(level + 1)}");
			}

			if (Genes.Count > 0)
			{
				s.Append('\n');

				for (int i = 0; i < level; i++)
				{
					s.Append("  ");
				}
			}

			return s.Append(')').ToString();
		}

		public override string ToString()
		{
			StringBuilder s = new StringBuilder($"({Value}");

			foreach (Gene gene in Genes)
			{
				s.Append($"{gene}");
			}

			return s.Append(')').ToString();
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;

				hash = hash * 29 + Value.GetHashCode();

				foreach (Gene gene in Genes)
				{
					hash = hash * 29 + gene.GetHashCode();
				}

				return hash;
			}
		}
	}
}
