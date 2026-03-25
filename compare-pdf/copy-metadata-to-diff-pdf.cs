using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string file1 = "original1.pdf";
        const string file2 = "original2.pdf";
        const string diffTemp = "diff_temp.pdf";
        const string diffOutput = "diff_with_metadata.pdf";

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("Source PDF files not found.");
            return;
        }

        // Load source PDFs and create a side‑by‑side comparison PDF (temporary file)
        using (Document doc1 = new Document(file1))
        using (Document doc2 = new Document(file2))
        {
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(doc1, doc2, diffTemp, options);
        }

        // Load the generated diff PDF and copy metadata from the first source document
        using (Document source = new Document(file1))
        using (Document diff = new Document(diffTemp))
        {
            diff.Info.Title = source.Info.Title;
            diff.Info.Author = source.Info.Author;
            diff.Info.Subject = source.Info.Subject;
            diff.Info.Keywords = source.Info.Keywords;
            diff.Info.Creator = source.Info.Creator;
            diff.Info.Producer = source.Info.Producer;
            diff.Info.CreationDate = source.Info.CreationDate;
            diff.Info.ModDate = source.Info.ModDate;
            diff.Info.Trapped = source.Info.Trapped;

            diff.Save(diffOutput);
        }

        // Clean up temporary file
        try { File.Delete(diffTemp); } catch { }

        Console.WriteLine($"Diff PDF saved to '{diffOutput}' with copied metadata.");
    }
}