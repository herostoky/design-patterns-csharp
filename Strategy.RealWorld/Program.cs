using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Strategy.RealWorld
{
    class OtpService
    {
        private IOtpGenerator _otpGenerator;

        public OtpService()
        {
            SetGenerator("Pin");
        }

        public OtpService(string generator)
        {
            SetGenerator(generator);
        }

        public void SetGenerator(string generator)
        {
            _otpGenerator = generator switch
            {
                "Pin" => new PinOtpGenerator(),
                "Hash" => new HashOtpGenerator(),
                _ => throw new ArgumentException("Invalid Otp generator"),
            };
        }

        public void GenerateOtp(string param)
        {
            Console.WriteLine("Context: Generating OTP using the strategy (not sure how it'll do it)");
            var result = _otpGenerator.GenerateOtp(param);

            Console.WriteLine(result);
        }
    }

    public interface IOtpGenerator
    {
        string GenerateOtp(string otpParam);
    }

    class PinOtpGenerator : IOtpGenerator
    {
        public string GenerateOtp(string otpParam)
        {
            var parsed = int.TryParse(otpParam, out var pinLength);
            if (!parsed || pinLength <= 0)
            {
                pinLength = 4;
            }
            var otp = Random.Shared.Next((int)Math.Pow(10, pinLength))
            .ToString()
            .PadLeft(pinLength, Random.Shared.Next(10).ToString()[0]);

            return otp;
        }
    }

    class HashOtpGenerator : IOtpGenerator
    {
        public string GenerateOtp(string otpParam)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new OtpService();

            Console.WriteLine("Client: Strategy is set to pin generation.");
            context.SetGenerator("Pin");
            context.GenerateOtp(string.Empty);

            Console.WriteLine();

            Console.WriteLine("Client: Strategy is set to hash generation.");
            context.SetGenerator("Hash");
            context.GenerateOtp(string.Empty);
        }
    }
}
