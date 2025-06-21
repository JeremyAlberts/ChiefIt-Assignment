export interface Order {
  milk: number;
  skins: number;
}

export interface OrderCommand {
  customer: string;
  order: Order;
}
