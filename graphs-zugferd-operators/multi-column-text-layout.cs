using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with a single blank page
        using (Aspose.Pdf.Document sampleDoc = new Aspose.Pdf.Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the sample PDF and add multi‑column text
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document("input.pdf"))
        {
            Aspose.Pdf.Page page = (Aspose.Pdf.Page)doc.Pages[1];

            // Create a FloatingBox to hold columnar content
            Aspose.Pdf.FloatingBox floatingBox = new Aspose.Pdf.FloatingBox();

            // Configure column information: 3 columns, 10‑point spacing, each 150 points wide
            Aspose.Pdf.ColumnInfo columnInfo = new Aspose.Pdf.ColumnInfo();
            columnInfo.ColumnCount = 3;
            columnInfo.ColumnSpacing = "10";
            columnInfo.ColumnWidths = "150 150 150";
            floatingBox.ColumnInfo = columnInfo;

            // Prepare a long text to flow through the columns
            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. ";
            string longText = "";
            for (int i = 0; i < 20; i++)
            {
                longText += lorem;
            }

            Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment(longText);
            textFragment.TextState.FontSize = 12;
            textFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");

            // Add the text fragment to the FloatingBox
            floatingBox.Paragraphs.Add(textFragment);

            // Add the FloatingBox to the page
            page.Paragraphs.Add(floatingBox);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}