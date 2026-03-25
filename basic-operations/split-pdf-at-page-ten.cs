using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string part1Path = "first_part.pdf";
        const string part2Path = "second_part.pdf";
        const int splitPage = 10;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document src = new Document(inputPath))
            {
                // First part: pages 1 through splitPage
                using (Document part1 = new Document())
                {
                    for (int i = 1; i <= splitPage && i <= src.Pages.Count; i++)
                    {
                        part1.Pages.Add(src.Pages[i]);
                    }
                    part1.Save(part1Path);
                }

                // Second part: pages after splitPage to the end
                using (Document part2 = new Document())
                {
                    for (int i = splitPage + 1; i <= src.Pages.Count; i++)
                    {
                        part2.Pages.Add(src.Pages[i]);
                    }
                    part2.Save(part2Path);
                }
            }

            Console.WriteLine($"PDF split completed: '{part1Path}', '{part2Path}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}