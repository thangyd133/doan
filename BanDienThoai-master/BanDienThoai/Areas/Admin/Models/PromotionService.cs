using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDienThoai.Domain.DataContext;

namespace BanDienThoai.Areas.Admin.Models
{
    public class PromotionService
    {
        BANDIENTHOAIEntities db;

        public PromotionService()
        {
            db = new BANDIENTHOAIEntities();
        }

        public IEnumerable<KHUYENMAI> getAllPromotion()
        {
            db = new BANDIENTHOAIEntities();
            return db.KHUYENMAIs;
        }

        public bool addPromotion(KHUYENMAI km)
        {
            try
            {
                db.KHUYENMAIs.Add(km);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool updatePromotion(KHUYENMAI km)
        {
            try
            {
                var result = db.KHUYENMAIs.Find(km.MAKM);
                if (result != null)
                {
                    result.TENKM = km.TENKM;
                    result.NGAYBD = km.NGAYBD;
                    result.NGAYKT = km.NGAYKT;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deletePromotion(string makm)
        {
            try
            {
                string query = "DELETE FROM CHITIETKM WHERE MAKM = '" + makm + "'";
                string query2 = "DELETE FROM KHUYENMAI WHERE MAKM = '" + makm + "'";
                db.Database.ExecuteSqlCommand(query);
                db.Database.ExecuteSqlCommand(query2);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string getLastRecord()
        {
            string res = "";
            var lastrecord = db.KHUYENMAIs.OrderByDescending(p => p.MAKM).FirstOrDefault();
            if (lastrecord != null)
            {
                res = lastrecord.MAKM;
            }

            return res;
        }

        public KHUYENMAI getPromotionById(string makm)
        {
            return db.KHUYENMAIs.Find(makm);
        }
    }
}