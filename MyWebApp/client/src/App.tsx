import './App.css'
import {Button} from './components/ui/button'
import {Card, CardContent, CardFooter, CardHeader, CardTitle} from './components/ui/card'

function App() {
    return (
        <>
            <Card>
                <CardHeader>
                    <CardTitle>Buy</CardTitle>
                </CardHeader>
                <CardContent>
                    <Button className="cursor-pointer">
                        Buy 1 corn
                    </Button>
                </CardContent>
                <CardFooter>
                    <p>Only 1 per minute</p>
                </CardFooter>
            </Card>

        </>
    )
}

export default App
