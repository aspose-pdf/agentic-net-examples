using System;
using Aspose.Pdf;

namespace AddGenerationDateFooter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new PDF document
            using (Document document = new Document())
            {
                // Add three blank pages to the document
                for (int i = 0; i < 3; i++)
                {
                    Page page = document.Pages.Add();
                }

                // Text to be used as footer (current date)
                string footerText = DateTime.Now.ToString("yyyy-MM-dd");

                // Add the footer to each page
                foreach (Page page in document.Pages)
                {
                    FooterArtifact footer = new FooterArtifact();
                    footer.Text = footerText;
                    footer.BottomMargin = 10;
                    footer.IsBackground = false;
                    page.Artifacts.Add(footer);
                }

                // Save the PDF document
                document.Save("output.pdf");
            }
        }
    }
}