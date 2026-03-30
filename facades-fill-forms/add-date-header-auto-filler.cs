using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled_with_header.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine("Template file not found: " + templatePath);
            return;
        }

        // Generate PDF using AutoFiller (no data table required for this example)
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.Save(outputPath);
        autoFiller.Close();

        // Add a header with the current date to each page of the generated PDF
        using (Document doc = new Document(outputPath))
        {
            string currentDate = DateTime.Now.ToString("d");
            foreach (Page page in doc.Pages)
            {
                TextFragment header = new TextFragment(currentDate);
                header.Position = new Position(50, page.PageInfo.Height - 50);
                header.TextState.FontSize = 12;
                header.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                page.Paragraphs.Add(header);
            }
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF generated with date header: " + outputPath);
    }
}