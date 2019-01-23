using ClassLibrary1;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("데이터베이스 테스트");
            MySQL my = new MySQL();

            //Member 테이블 검색
            MySqlDataReader sdr = my.Reader("select * from Member;");
            int count = 0;
            while (sdr.Read())
            {
                count++;
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    //Console.Write(sdr.GetName(i) + ":" + sdr.GetValue(i) + " ");
                }
                //Console.WriteLine();
            }
            Console.WriteLine(count);
            my.ReaderClose(sdr);

            //Member 테이블에 데이터 입력
            int result = my.NonQuery("insert into Member (mId,mPass,mName) values ('입력','테스','트1');");
            if (result > 0)
            {
                Console.WriteLine("적용된행 :{0}개 입력 성공...!!",result);
            }
            else
            {
                Console.WriteLine("적용된행 :{0}개 입력 실패..", result);
            }

            //위에 입력한 행의 mNo구해오기
            string sql = "select max(mNo) as mNo from Member;";
            sdr = my.Reader(sql);
            int mNo = 0;
            while(sdr.Read())
            {
                mNo = Convert.ToInt32(sdr["mNo"]);
            }
            sdr.Close();
            Console.WriteLine("mNo => {0}",mNo);

            //Member 테이블에 데이터 수정
            result = my.NonQuery(string.Format("update Member set mId = '입력',mPass='테스',mName='트2',modDate=NOW() where mNo={0};", mNo));
            if (result > 0)
            {
                Console.WriteLine("적용된행 :{0}개 수정 성공...!!", result);
            }
            else
            {
                Console.WriteLine("적용된행 :{0}개 수정 실패..", result);
            }

            //Member 테이블에 데이터 삭제
            result = my.NonQuery(string.Format("update Member set delYn='Y',modDate=NOW() where mNo={0};", mNo));
            if ( result > 0)
            {
                Console.WriteLine("적용된행 :{0}개 삭제 성공...!!", result);
            }
            else
            {
                Console.WriteLine("적용된행 :{0}개 삭제 실패..", result);
            }

            //Member 테이블 재검색
            MySqlDataReader sdr1 = my.Reader("select * from Member;");
            while (sdr1.Read())
            {
                for (int i = 0; i < sdr1.FieldCount; i++)
                {
                    Console.Write(sdr1.GetName(i) + ":" + sdr1.GetValue(i) + " ");
                }
                Console.WriteLine();
            }
            my.ReaderClose(sdr1);

            my.Close();

        }
    }
}
