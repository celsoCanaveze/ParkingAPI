using Microsoft.ML;
using ParkingAPI.Models;

namespace ParkingAPI.Services
{
    public class MLService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public MLService()
        {
            _mlContext = new MLContext();
            // Modelo de exemplo (simulado)
            // Você poderia carregar um modelo real aqui: _mlContext.Model.Load("MLModels/ModeloTreinado.zip", out _)
        }

        public PredictOutput Predict(PredictInput input)
        {
            var valorPrevisto = input.TempoEstacionado * input.ValorPorHora;
            return new PredictOutput { ValorPrevisto = valorPrevisto };
        }
    }
}
