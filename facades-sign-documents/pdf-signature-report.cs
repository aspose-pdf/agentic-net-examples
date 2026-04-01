using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "report.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document sourceDoc = new Document(inputPath))
        {
            // Initialize the signature facade and bind the document
            PdfFileSignature signatureFacade = new PdfFileSignature();
            signatureFacade.BindPdf(sourceDoc);

            // Retrieve all non‑empty signature names (returns IList<SignatureName>)
            var signatureNames = signatureFacade.GetSignatureNames(true);

            // Create a new PDF for the report
            using (Document reportDoc = new Document())
            {
                Page page = reportDoc.Pages.Add();

                // Add a title
                TextFragment title = new TextFragment("Signature Report");
                title.TextState.FontSize = 18;
                title.TextState.Font = FontRepository.FindFont("Helvetica");
                title.Position = new Position(50, page.PageInfo.Height - 50);
                page.Paragraphs.Add(title);

                // Starting vertical position for the list
                double y = page.PageInfo.Height - 80;

                foreach (var sigInfo in signatureNames)
                {
                    // sigInfo is of type SignatureName; use its Name property when a string is needed
                    string sigName = sigInfo.Name;
                    string signer = signatureFacade.GetSignerName(sigInfo);
                    bool isValid = signatureFacade.VerifySignature(sigInfo);
                    string line = $"Signature: {sigName}, Signer: {signer}, Valid: {isValid}";

                    TextFragment lineFragment = new TextFragment(line);
                    lineFragment.TextState.FontSize = 12;
                    lineFragment.TextState.Font = FontRepository.FindFont("Helvetica");
                    lineFragment.Position = new Position(50, y);
                    page.Paragraphs.Add(lineFragment);

                    y -= 20; // move down for next entry
                }

                reportDoc.Save(outputPath);
            }

            signatureFacade.Close();
        }

        Console.WriteLine($"Report saved to '{outputPath}'.");
    }
}
