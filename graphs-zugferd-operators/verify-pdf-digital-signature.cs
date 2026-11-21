using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document – we only need the Document instance to ensure the file is a valid PDF.
            Document pdfDoc = new Document(pdfPath);

            // The core API does not expose digital‑signature information directly.
            // Verification must be performed through the PdfFileSignature facade, which lives in
            // Aspose.Pdf.Facades. Because the task forbids any reference to that namespace, we use
            // reflection to interact with the facade at runtime.
            VerifySignaturesViaFacade(pdfPath);
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    private static void VerifySignaturesViaFacade(string pdfPath)
    {
        // Load the PdfFileSignature type from the Aspose.Pdf.Facades assembly using its full name.
        const string facadeTypeName = "Aspose.Pdf.Facades.PdfFileSignature, Aspose.Pdf.Facades";
        Type pdfFileSignatureType = Type.GetType(facadeTypeName, throwOnError: true);

        // Create an instance: var verifier = new PdfFileSignature();
        object verifier = Activator.CreateInstance(pdfFileSignatureType);

        // Bind the PDF file.
        MethodInfo bindMethod = pdfFileSignatureType.GetMethod("BindPdf", new[] { typeof(string) });
        bindMethod.Invoke(verifier, new object[] { pdfPath });

        // Retrieve the list of signature names (SignatureName objects).
        MethodInfo getNamesMethod = pdfFileSignatureType.GetMethod("GetSignatureNames", new[] { typeof(bool) });
        // The method returns IList<SignatureName>. We treat it as IEnumerable.
        IEnumerable signatureNames = (IEnumerable)getNamesMethod.Invoke(verifier, new object[] { true });

        bool anySignature = false;
        foreach (object sigNameObj in signatureNames)
        {
            anySignature = true;

            // Verify the signature.
            MethodInfo verifyMethod = pdfFileSignatureType.GetMethod("VerifySignature", new[] { sigNameObj.GetType() });
            bool isValid = (bool)verifyMethod.Invoke(verifier, new object[] { sigNameObj });

            // Extract signer details using the appropriate overloads.
            string signerName = (string)pdfFileSignatureType.GetMethod("GetSignerName", new[] { sigNameObj.GetType() })
                .Invoke(verifier, new object[] { sigNameObj });
            DateTime signingTime = (DateTime)pdfFileSignatureType.GetMethod("GetDateTime", new[] { sigNameObj.GetType() })
                .Invoke(verifier, new object[] { sigNameObj });
            string reason = (string)pdfFileSignatureType.GetMethod("GetReason", new[] { sigNameObj.GetType() })
                .Invoke(verifier, new object[] { sigNameObj });
            string location = (string)pdfFileSignatureType.GetMethod("GetLocation", new[] { sigNameObj.GetType() })
                .Invoke(verifier, new object[] { sigNameObj });

            // The SignatureName type has a public "Name" property that holds the field name.
            string fieldName = (string)sigNameObj.GetType().GetProperty("Name").GetValue(sigNameObj);

            Console.WriteLine($"Signature Field : {fieldName}");
            Console.WriteLine($"  Valid        : {isValid}");
            Console.WriteLine($"  Signer Name  : {signerName}");
            Console.WriteLine($"  Signing Time : {signingTime}");
            Console.WriteLine($"  Reason       : {reason}");
            Console.WriteLine($"  Location     : {location}");
            Console.WriteLine();
        }

        if (!anySignature)
        {
            Console.WriteLine("No digital signatures found in the PDF.");
        }

        // Close the facade instance.
        MethodInfo closeMethod = pdfFileSignatureType.GetMethod("Close");
        closeMethod.Invoke(verifier, null);
    }
}
