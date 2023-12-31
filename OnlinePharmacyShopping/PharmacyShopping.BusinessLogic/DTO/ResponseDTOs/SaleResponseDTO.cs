﻿namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class SaleResponseDTO
    {
        public int SaleId { get; set; }

        public string PharmacyName { get; set; }

        public string CustomerFullName { get; set; }

        public List<string> MedicineNameAndTheirAmount { get; set; }

        public List<int> PurchaseId { get; set; }

        public DateTime SaleDate { get; set; }

        public double TotalPrice { get; set; }
    }
}