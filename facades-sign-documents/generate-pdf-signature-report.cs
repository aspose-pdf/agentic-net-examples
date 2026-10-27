using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class SignatureReportGenerator
{
    static void Main()
    {
        const string inputPdfPath = "signed.pdf";
        const string reportPdfPath = "signature_report.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF for signature operations (newer constructor pattern)
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdfPath);

            // Load the document to access signature fields (SignatureInfo class is no longer exposed)
            Document srcDoc = new Document(inputPdfPath);

            // Retrieve signature fields via the generic Fields collection
            var signatureFields = srcDoc.Form?.Fields?.OfType<SignatureField>()?.ToList() ?? new System.Collections.Generic.List<SignatureField>();
            int signatureCount = signatureFields.Count;

            // Prepare a new PDF document that will hold the report
            using (Document reportDoc = new Document())
            {
                // Add a page for the report content
                Page reportPage = reportDoc.Pages.Add();

                // Title of the report
                TextFragment title = new TextFragment("Signature Verification Report");
                title.TextState.FontSize = 18;
                title.TextState.FontStyle = FontStyles.Bold;
                title.TextState.Font = FontRepository.FindFont("Helvetica");
                title.Margin = new MarginInfo(10, 0, 10, 0);
                reportPage.Paragraphs.Add(title);

                // Create a table with four columns
                Table table = new Table
                {
                    ColumnWidths = "50 200 200 80",
                    Border = new BorderInfo(BorderSide.All, 0.5f)
                };

                // Helper to create a cell containing a simple text fragment
                Cell CreateCell(string text)
                {
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment(text));
                    return cell;
                }

                // Header row
                Row header = table.Rows.Add();
                header.Cells.Add(CreateCell("No."));
                header.Cells.Add(CreateCell("Signer"));
                header.Cells.Add(CreateCell("Signing Time"));
                header.Cells.Add(CreateCell("Valid"));

                // Iterate over each signature field and populate the table
                for (int i = 0; i < signatureCount; i++)
                {
                    var sigField = signatureFields[i];

                    // Try to extract the certificate to obtain signer information
                    string signer = "Unknown";
                    try
                    {
                        X509Certificate2 cert = sigField.ExtractCertificateObject();
                        if (cert != null)
                            signer = cert.Subject;
                    }
                    catch { /* certificate extraction not supported – keep "Unknown" */ }

                    // Signing time – some versions expose it via SignatureInfo, otherwise fallback to N/A
                    string signingTime = "N/A";
                    try
                    {
                        var infoProp = sigField.GetType().GetProperty("SignatureInfo");
                        if (infoProp != null)
                        {
                            var info = infoProp.GetValue(sigField);
                            var timeProp = info?.GetType().GetProperty("SigningTime");
                            var timeVal = timeProp?.GetValue(info) as DateTime?;
                            if (timeVal.HasValue)
                                signingTime = timeVal.Value.ToString("g");
                        }
                    }
                    catch { /* ignore – keep N/A */ }

                    // Verify the signature using the facade (field name based verification)
                    bool isValid = false;
                    try
                    {
                        // Use the field's full name for verification
                        string fieldName = sigField.FullName ?? sigField.Name;
                        if (!string.IsNullOrEmpty(fieldName))
                            isValid = pdfSignature.VerifySignature(fieldName);
                    }
                    catch { /* verification not supported – treat as false */ }

                    // Add a data row to the table
                    Row row = table.Rows.Add();
                    row.Cells.Add(CreateCell((i + 1).ToString()));
                    row.Cells.Add(CreateCell(signer));
                    row.Cells.Add(CreateCell(signingTime));
                    row.Cells.Add(CreateCell(isValid ? "Yes" : "No"));
                }

                // Add the populated table to the page
                reportPage.Paragraphs.Add(table);

                // Save the report PDF
                reportDoc.Save(reportPdfPath);
            }
        }

        Console.WriteLine($"Signature report generated: {reportPdfPath}");
    }
}
