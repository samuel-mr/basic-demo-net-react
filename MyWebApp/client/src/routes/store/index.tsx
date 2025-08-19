import { StoreProcess } from '@/feature/store/storeProcess'
import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/store/')({
  component: RouteComponent,
})

function RouteComponent() {
  return <StoreProcess  ></StoreProcess>
}
