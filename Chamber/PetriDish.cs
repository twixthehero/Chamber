using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chamber
{
	public class PetriDish
	{
		private int CurrentGen { get; set; } = 0;
		private int CurrentGenHash { get; set; }
		private Dictionary<int, GeneSequence> Generations = new Dictionary<int, GeneSequence>();
		private int LatestGen { get; set; } = 0;

		public PetriDish(GeneSequence original)
		{
			Generations.Add(CurrentGen, original);
			CurrentGenHash = original.GetHashCode();
		}

		public void Iterate(int count)
		{
			GeneSequence child;
			int childHash;

			for (int i = 0; i < count; i++)
			{
				do
				{
					child = Generations[LatestGen].Copy();
					childHash = child.GetHashCode();
					CurrentGen++;
				}
				while (childHash == CurrentGenHash); // while we haven't deviated

				Generations.Add(CurrentGen, child);
				CurrentGenHash = childHash;
				LatestGen = CurrentGen;
			}
		}

		public void PrintData()
		{
			foreach (KeyValuePair<int, GeneSequence> pair in Generations)
			{
				Console.WriteLine($"{pair.Key}: {pair.Value}");
			}
		}

		public void SaveData(string file)
		{
			using (FileStream fs = File.Open(file, FileMode.Create, FileAccess.Write, FileShare.Read))
			using (StreamWriter w = new StreamWriter(fs, Encoding.Unicode))
			{
				foreach (KeyValuePair<int, GeneSequence> pair in Generations)
				{
					w.WriteLine($"=== {pair.Key} =============================================");
					w.WriteLine(pair.Value.Start.ToStringFormatted());
				}
			}
		}
	}
}
