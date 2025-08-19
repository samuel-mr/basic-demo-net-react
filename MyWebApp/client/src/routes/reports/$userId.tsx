import { InternalReport } from '@/feature/reports/internalReport';
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/reports/$userId')({
  component: RouteComponent,
  loader: async ({params})=>{
    return {
      id: params.userId,
    };
  }
})

function RouteComponent() {
  const {id} = Route.useLoaderData();
  return <div>
    <InternalReport userId={id}></InternalReport>
    </div>
}
