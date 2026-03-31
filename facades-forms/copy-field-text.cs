using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            Form form = new Form(document);
            string notesValue = form.GetField("Notes");
            if (notesValue == null)
            {
                Console.WriteLine("Field 'Notes' not found or empty.");
            }
            else
            {
                form.FillField("Summary", notesValue);
                Console.WriteLine("Copied value from 'Notes' to 'Summary'.");
            }

            document.Save(outputPath);
        }

        Console.WriteLine("Saved updated PDF to '" + outputPath + "'.");
    }
}