import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import toast from "react-hot-toast";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { z } from "zod";
import { useForm } from "react-hook-form";

type FormValues = {
  UserId: string;
  Quantity: number;
};
const FormSchema = z.object({
  UserId: z.string().min(2, {
    message: "Username must be at least 2 characters.",
  }),
  Quantity: z.number().min(1, {
    message: "Quantity must be at least 1.",
  }),
});

export function StoreProcess() {
  const form = useForm<FormValues>({
    resolver: zodResolver(FormSchema),
    defaultValues: {
      UserId: "",
      Quantity: 0,
    },
  });

  function onSubmit(data: FormValues) {
    console.log("sending...");
    fetch("https://localhost:5001/api/order/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then(async(response) => {
        if (!response.ok) {
          return response.json().then((errorData) => {
            if (errorData?.status === 429) {
              throw new Error(
                "You are allowed to but 1 per minute. Please wait."
              );
            } else {
              throw new Error(errorData.detail || errorData);
            }
          });
        }
        const text = await response.text();
        if (!text) {
          return null; // o return {} si prefieres un objeto vacío
        }
        return JSON.parse(text);
      })
      .then(() => {
        toast.success("Successful purchase", {
          position: "bottom-center",
        });
        form.reset();
      })
      .catch((error) => {
        toast.error(error.message, {
          position: "bottom-center",
        });
      });
  }

  return (
    <>
      <Card>
        <CardHeader>
          <CardTitle>Bob's Corn</CardTitle>
        </CardHeader>
        <CardContent>
          <Form {...form}>
            <form
              onSubmit={form.handleSubmit(onSubmit)}
              className="w-2/3 space-y-6"
            >
              <FormField
                control={form.control}
                name="UserId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Username</FormLabel>
                    <FormControl>
                      <Input placeholder="see the list below" {...field} />
                    </FormControl>
                    <FormDescription>
                      In shake of simplicity : name = userId
                    </FormDescription>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="Quantity"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Quantity</FormLabel>
                    <FormControl>
                      <Input
                        type="number"
                        placeholder="Enter quantity"
                        {...field}
                        onChange={(e) =>
                          field.onChange(e.target.valueAsNumber || 0)
                        }
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <Button type="submit">Submit</Button>
            </form>
          </Form>
        </CardContent>
        <CardFooter>
          <p>Available Users: Bach, Mozart, Pachelbel</p>
        </CardFooter>
      </Card>
    </>
  );
}
