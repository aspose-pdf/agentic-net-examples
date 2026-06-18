using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create sample PDF files (self‑contained example)
        string[] inputFiles = new string[] { "sample1.pdf", "sample2.pdf" };
        foreach (string file in inputFiles)
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(file);
            }
        }

        // Loop through each PDF, add custom metadata "Department", and save the result
        foreach (string file in inputFiles)
        {
            using (PdfFileInfo pdfInfo = new PdfFileInfo(file))
            {
                pdfInfo.SetMetaInfo("Department", "Finance");
                string outputFile = System.IO.Path.GetFileNameWithoutExtension(file) + "_updated.pdf";
                pdfInfo.SaveNewInfo(outputFile);
            }
        }
    }
}