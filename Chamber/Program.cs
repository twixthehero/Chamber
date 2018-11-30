using System;
using System.Text;

namespace Chamber
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;

			GeneSequence sequence = new GeneSequence('A');
			for (int i = 0; i < 2; i++)
			{
				sequence.Extend((char)('B' + i));
			}

			PetriDish dish = new PetriDish(sequence);
			dish.Iterate(100);
			//dish.PrintData();
			dish.SaveData("test.txt");
		}
	}
}
