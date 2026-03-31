using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string csvPath = "mapping.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine("Mapping CSV not found: " + csvPath);
            return;
        }

        Form form = new Form(inputPdf, outputPdf);

        string[] lines = File.ReadAllLines(csvPath);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length != 2)
                continue;

            string oldName = parts[0].Trim();
            string newName = parts[1].Trim();
            if (oldName.Length == 0 || newName.Length == 0)
                continue;

            form.RenameField(oldName, newName);
        }

        form.Save();
        Console.WriteLine("Fields renamed and saved to " + outputPdf);
    }
}
