using System;
using alms.cherry.data.objects.Entities.Product;

namespace alms.cherry.data.objects.Extensions
{
    public static class ProductExtension
    {
        public static Product GetUpdated(this Product original, Product toCompare)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            if (toCompare == null)
            {
                return original;
            }
            if (original.Id == toCompare.Id)
            {
                toCompare.AdditionalInformation = original.AdditionalInformation != toCompare.AdditionalInformation
                    ? original.AdditionalInformation
                    : toCompare.AdditionalInformation;

                toCompare.ProductName = original.ProductName != toCompare.ProductName
                    ? original.ProductName
                    : toCompare.ProductName;

                toCompare.ProductCategoryId = original.ProductCategoryId != toCompare.ProductCategoryId
                    ? original.ProductCategoryId
                    : toCompare.ProductCategoryId;

                toCompare.ProductTypeId = original.ProductTypeId != toCompare.ProductTypeId
                    ? original.ProductTypeId
                    : toCompare.ProductTypeId;

                toCompare.ProductUnitId = original.ProductUnitId != toCompare.ProductUnitId
                    ? original.ProductUnitId
                    : toCompare.ProductUnitId;

                toCompare.Quantity = original.Quantity != toCompare.Quantity ? original.Quantity : toCompare.Quantity;

                toCompare.VatGroupId = original.VatGroupId != toCompare.VatGroupId
                    ? original.VatGroupId
                    : toCompare.VatGroupId;
            }
            else
            {
                throw new Exception("Both objects have different ids");
            }

            return toCompare;
        }
    }
}
