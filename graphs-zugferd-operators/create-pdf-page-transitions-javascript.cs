using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "PageTransitions.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add three pages with different transition effects using JavaScript actions
            for (int i = 1; i <= 3; i++)
            {
                // Add a blank page
                Page page = doc.Pages.Add();

                // Add simple text to identify the page
                TextFragment tf = new TextFragment($"Page {i}");
                tf.TextState.FontSize = 24;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Color.Blue;
                page.Paragraphs.Add(tf);

                // Define JavaScript that sets the page transition when the page is opened
                string jsCode = i switch
                {
                    1 => "this.setPageTransition({type:'Dissolve', duration:2});",
                    2 => "this.setPageTransition({type:'Split', duration:3});",
                    3 => "this.setPageTransition({type:'Box', duration:1});",
                    _ => string.Empty
                };

                // Attach the JavaScript action to the page's OnOpen event
                page.Actions.OnOpen = new JavascriptAction(jsCode);
            }

            // Save the PDF with the defined page transitions
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}
