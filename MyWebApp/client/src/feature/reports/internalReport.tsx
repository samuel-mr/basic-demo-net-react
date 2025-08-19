
import { useEffect, useState } from "react";
 import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"

export interface GetPurchasedOrdersResponse {
    userId: string;
    orders: OrderDto[];
}

export interface OrderDto {
    id: string;
    quantity: number;
    status: string;
    createdAt: string;
}

interface InternalReportProps{
  userId: string
}
export function InternalReport({userId} : InternalReportProps) {
    const [data, setData] = useState<GetPurchasedOrdersResponse>();
    console.log("sending...");
    useEffect(() => {
        fetch("https://localhost:5001/api/orders/" + userId)
            .then(res => res.json())
            .then(setData)
            .catch(console.error);
}, [userId]);

if(!userId)
  return "";

  return (
    <>
    <Table>
  <TableCaption>A list of your recent purchased items</TableCaption>
  <TableHeader>
    <TableRow>
      <TableHead className="w-[100px]">Invoice</TableHead>
      <TableHead>Time</TableHead>
    </TableRow>
  </TableHeader>
  <TableBody>
  {data?.orders && data.orders.map(element => 
     <TableRow>
      <TableCell className="font-medium">{element.id}</TableCell>
      <TableCell>{element.createdAt}</TableCell>
    </TableRow>
  )}
   
  </TableBody>
</Table>
    </>
  );
}
