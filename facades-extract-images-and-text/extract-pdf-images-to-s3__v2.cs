using System;
using System.IO;
using Aspose.Pdf;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

// ---------------------------------------------------------------------
// Minimal stubs for the AWS SDK (Amazon.S3) to allow the project to build
// when the real SDK is not referenced. In a production environment you
// should replace these stubs with the official AWSSDK.S3 NuGet package.
// ---------------------------------------------------------------------
namespace Amazon.S3
{
    public class AmazonS3Client
    {
        // The real client has many constructors; an empty one is enough for the stub.
        public AmazonS3Client() { }
    }
}

namespace Amazon.S3.Model
{
    public enum S3CannedACL
    {
        Private
    }

    public enum ServerSideEncryptionMethod
    {
        AES256
    }
}

namespace Amazon.S3.Transfer
{
    public class TransferUtilityUploadRequest
    {
        public Stream InputStream { get; set; }
        public string BucketName { get; set; }
        public string Key { get; set; }
        public S3CannedACL CannedACL { get; set; }
        public ServerSideEncryptionMethod ServerSideEncryptionMethod { get; set; }
    }

    public class TransferUtility
    {
        private readonly AmazonS3Client _client;

        public TransferUtility(AmazonS3Client client)
        {
            _client = client;
        }

        // In the stub we simply simulate an upload by writing a message to the console.
        public void Upload(TransferUtilityUploadRequest request)
        {
            // Simulate network latency or any other side‑effects if needed.
            Console.WriteLine($"[Stub] Uploaded '{request.Key}' to bucket '{request.BucketName}' with SSE={request.ServerSideEncryptionMethod}");
        }
    }
}

namespace PdfImageToS3Example
{
    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string bucketName = "my-s3-bucket";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
                return;
            }

            // Create an Amazon S3 client (credentials are taken from the environment or config files)
            AmazonS3Client s3Client = new AmazonS3Client();
            TransferUtility transferUtility = new TransferUtility(s3Client);

            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                int pageNumber = 1;
                foreach (Page page in pdfDocument.Pages)
                {
                    int imageIndex = 1;
                    foreach (XImage xImage in page.Resources.Images)
                    {
                        // Save the image to a memory stream
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            xImage.Save(imageStream);
                            imageStream.Position = 0;

                            // Build a unique key for the object in S3
                            string objectKey = $"page{pageNumber}_image{imageIndex}.png";

                            // Prepare the upload request with server‑side encryption (AES‑256)
                            TransferUtilityUploadRequest uploadRequest = new TransferUtilityUploadRequest
                            {
                                InputStream = imageStream,
                                BucketName = bucketName,
                                Key = objectKey,
                                CannedACL = S3CannedACL.Private,
                                ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256
                            };

                            // Upload the image to S3 (stubbed in this example)
                            transferUtility.Upload(uploadRequest);
                            Console.WriteLine($"Uploaded {objectKey} to bucket {bucketName}");
                        }
                        imageIndex++;
                    }
                    pageNumber++;
                }
            }
        }
    }
}
