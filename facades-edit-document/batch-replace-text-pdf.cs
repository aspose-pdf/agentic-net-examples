using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputFolder = "input";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfPath);
            using (Document doc = new Document(pdfPath))
            {
                TextFragmentAbsorber absorber = new TextFragmentAbsorber("Confidential");
                doc.Pages.Accept(absorber);
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.Text = "Public";
                }
                doc.Save(fileName);
            }
            Console.WriteLine($"Processed: {fileName}");
        }
    }
}