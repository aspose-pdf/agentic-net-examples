using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source form PDF, XML data and the desired output PDF
        const string formPdfPath = "form.pdf";
        const string dataXmlPath = "data.xml";
        const string outputPdfPath = "filled_output.pdf";

        // Verify that required input files exist
        if (!File.Exists(formPdfPath) || !File.Exists(dataXmlPath))
        {
            Console.Error.WriteLine("Form PDF or XML data file not found.");
            return;
        }

        // Load the PDF form document
        using (Document pdfDoc = new Document(formPdfPath))
        {
            // Import XML data into the form fields using the Form facade
            using (Form formFacade = new Form(pdfDoc))
            {
                using (FileStream xmlStream = File.OpenRead(dataXmlPath))
                {
                    formFacade.ImportXml(xmlStream);
                }
            }

            // Prepare the date string that will appear on every page
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Add the header to each page of the document
            foreach (Page page in pdfDoc.Pages)
            {
                TextFragment dateFragment = new TextFragment(currentDate)
                {
                    TextState =
                    {
                        FontSize = 12,
                        Font = FontRepository.FindFont("Arial"),
                        ForegroundColor = Aspose.Pdf.Color.Black
                    }
                };
                // Position the header near the top (50 points from left, 50 points from top)
                dateFragment.Position = new Position(50, page.PageInfo.Height - 50);
                page.Paragraphs.Add(dateFragment);
            }

            // Save the filled PDF with the header added
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
    }
}
